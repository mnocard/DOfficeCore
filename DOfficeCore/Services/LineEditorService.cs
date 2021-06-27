using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using Serilog;

namespace DOfficeCore.Services
{
    ///<inheritdoc cref="ILineEditorService"/>
    class LineEditorService : ILineEditorService
    {
        private const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

        ///<inheritdoc/>
        public string OpenDocument(string filepath)
        {
            return OpenDocumentAsync(filepath).Result;
        }

        ///<inheritdoc/>
        public async Task<string> OpenDocumentAsync(string filepath, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return string.Empty;
            }

            var task = await Task.Run(() =>
            {
                var textBuilder = new StringBuilder();
                string result;
                WordprocessingDocument wdDoc = null;

                try
                {
                    wdDoc = WordprocessingDocument.Open(filepath, false);
                    var nt = new NameTable();
                    var nsManager = new XmlNamespaceManager(nt);
                    nsManager.AddNamespace("w", wordmlNamespace);

                    var xdoc = new XmlDocument(nt);
                    xdoc.Load(wdDoc.MainDocumentPart.GetStream());

                    XmlNodeList paragraphNodes = xdoc.SelectNodes("//w:p", nsManager);
                    foreach (XmlNode paragraphNode in paragraphNodes)
                    {
                        textBuilder.Append(paragraphNode.InnerText);
                        if(token.IsCancellationRequested)
                        {
                            token.ThrowIfCancellationRequested();
                        }
                    }
                    result = textBuilder.ToString();
                }
                catch(IOException e)
                {
                    Log.Error($"Ошибка! IOException: файл уже используется другой программой.\n{0}", e.Message);
                    throw new Exception("Unexpected error", e);
                }
                catch (InvalidDataException e)
                {
                    Log.Error($"Ошибка! InvalidDataException.\n{0}", e.Message);
                    throw new Exception($"Cannot open file \"{filepath}\"", e);
                }
                catch (OperationCanceledException e)
                {
                    Log.Error($"Ошибка! OperationCanceledException.\n{0}", e.Message);
                    throw new Exception("Unexpected error", e);
                }
                catch (Exception e)
                {
                    Log.Error($"Непредвиденная ошибка!\n{0}", e.Message);
                    throw new Exception("Unexpected error", e);
                }
                finally
                {
                    wdDoc?.Dispose();
                }

                return result;
            }).ConfigureAwait(false);

            return task;
        }

        ///<inheritdoc/>
        public List<string> TextToLines(string lines) => string.IsNullOrWhiteSpace(lines) ? null : TextToLinesAsync(lines).Result;

        ///<inheritdoc/>
        public async Task<List<string>> TextToLinesAsync(string lines, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(lines)) throw new ArgumentNullException(nameof(lines));

            var task = await Task.Run(() =>
            {
                lines = Regex.Replace(lines, @"(\d+)([.|,])(\d+)([.|,])(\d+)([г][.|,])|(\d+)([.|,])(\d+)([г][.|,])|(\d+)([г][.|,])|(\d+)([г])|(\d+)([.|,])(\d+)([.|,])(\d+)|(\d+)([.|,])(\d+)", "");
                lines = Regex.Replace(lines, @"(\s+)", " ");

                var words = lines.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (var i = 0; i < words.Count;)
                {
                    string line = words[i].Trim();
                    if (line.Length > 7)
                    {
                        line = line.Replace("  ", " ");
                        line = line.ToLower();
                        line = char.ToUpper(line[0]) + line.Substring(1);
                        words[i] = line + ".";
                        i++;
                    }
                    else
                    {
                        words.RemoveAt(i);
                    }

                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                }

                return words.Select(w => w).Distinct().ToList();
            }).ConfigureAwait(false);

            return task;
        }

        ///<inheritdoc/>
        public List<string> OpenAndConvert(string filepath) => TextToLines(OpenDocument(filepath));
    }
}
