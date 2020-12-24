using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DOfficeCore.Logger;

namespace DOfficeCore.Services
{
    ///<inheritdoc cref="ILineEditorService"/>
    class LineEditorService : ILineEditorService
    {
        private const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        private readonly ILogger _Logger;
        public LineEditorService(ILogger Logger) => _Logger = Logger;

        ///<inheritdoc/>
        public string OpenDocument(string filepath)
        {
            _Logger.WriteLog($"Trying to open document {filepath} and read it.");

            if (string.IsNullOrEmpty(filepath))
            {
                _Logger.WriteLog($"{filepath} doesn't exist.");
                return string.Empty;
            }

            const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

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
                }
                result = textBuilder.ToString();
            }
            catch (System.IO.InvalidDataException e)
            {
                _Logger.WriteLog($"Can't open {filepath}. InvalidDataException:\n{e.Message}");

                throw new Exception($"Cannot open file \"{filepath}\"");
            }
            catch (Exception e)
            {
                _Logger.WriteLog($"Can't open {filepath}. Exception:\n{e.Message}");

                throw new Exception("Unexpected exception\n" + e.Message);
            }
            finally
            {
                wdDoc?.Dispose();
            }

            _Logger.WriteLog($"Succes.");

            return result;
        }

        ///<inheritdoc/>
        public async Task<string> OpenDocumentAsync(string filepath, CancellationToken token)
        {
            _Logger.WriteLog($"Trying to async open document {filepath} and read it.");

            if (string.IsNullOrEmpty(filepath))
            {
                _Logger.WriteLog($"{filepath} doesn't exist.");
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
                catch (System.IO.InvalidDataException e)
                {
                    _Logger.WriteLog($"Can't open {filepath}. InvalidDataException:\n{e.Message}");

                    throw new Exception($"Cannot open file \"{filepath}\"", e);
                }
                catch (OperationCanceledException e)
                {
                    _Logger.WriteLog($"Operation cancelled:\n{e.Message}");
                    throw;
                }
                catch (Exception e)
                {
                    _Logger.WriteLog($"Can't open {filepath}. Exception:\n{e.Message}");

                    throw new Exception("Unexpected exception.", e);
                }
                finally
                {
                    wdDoc?.Dispose();
                }

                _Logger.WriteLog($"Succes.");

                return result;
            }).ConfigureAwait(false);

            return task;
        }

        ///<inheritdoc/>
        public List<string> TextToLines(string lines)
        {
            _Logger.WriteLog($"Trying to convert text to list of lines.");

            if (string.IsNullOrEmpty(lines)) throw new ArgumentNullException();

            lines = Regex.Replace(lines, @"(\d+)([.|,])(\d+)([.|,])(\d+)([г][.|,])|(\d+)([.|,])(\d+)([г][.|,])|(\d+)([г][.|,])|(\d+)([г])|(\d+)([.|,])(\d+)([.|,])(\d+)|(\d+)([.|,])(\d+)", "");
            lines = Regex.Replace(lines, @"(\s+)", " ");

            var words = lines.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (var i = 0; i < words.Count;)
            {
                string line = words[i].Trim();
                if(line.Length > 7)
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
            }

            _Logger.WriteLog($"Converting done.");

            return words;
        }

        ///<inheritdoc/>
        public async Task<List<string>> TextToLinesAsync(string lines, CancellationToken token)
        {
            _Logger.WriteLog($"Trying to async convert text to list of lines.");

            if (string.IsNullOrEmpty(lines)) throw new ArgumentNullException();

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

                _Logger.WriteLog($"Converting done.");

                return words;
            }).ConfigureAwait(false);

            return task;
        }
    }
}
