using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DOfficeCore.Services
{
    class LineEditorService : ILineEditorService
    {
        /// <summary>
        /// Получение из файла формата docx текста
        /// </summary>
        /// <param name="filepath">Путь к файлу</param>
        /// <returns>Содержимое файла</returns>
        public string OpenDocument(string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) return string.Empty;
            
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

                var paragraphNodes = xdoc.SelectNodes("//w:p", nsManager);
                foreach (XmlNode paragraphNode in paragraphNodes)
                {
                    textBuilder.Append(paragraphNode.InnerText);
                }
                result = textBuilder.ToString();
            }
            catch (System.IO.InvalidDataException e)
            {
                throw new Exception($"Cannot open file \"{filepath}\"", e);
            }
            catch (Exception e)
            {
                throw new Exception("Unexpected exception.", e);
            }
            finally
            {
                wdDoc?.Dispose();
            }

            return result;
        }
        
        /// <summary>
        /// Преобразование текста в список предложений с удалением всех дат
        /// </summary>
        /// <param name="lines">Текст для преобразования</param>
        /// <returns>Список предложений, полученных из текста</returns>
        public List<string> TextToLines(string lines)
        {
            if (string.IsNullOrEmpty(lines)) throw new ArgumentNullException();

            lines = Regex.Replace(lines, @"(\d+)([.|,])(\d+)([.|,])(\d+)([г][.|,])|(\d+)([.|,])(\d+)([г][.|,])|(\d+)([г][.|,])|(\d+)([г])|(\d+)([.|,])(\d+)([.|,])(\d+)|(\d+)([.|,])(\d+)", "");
            lines = Regex.Replace(lines, @"(\s+)", " ");

            var words = lines.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (var i = 0; i < words.Count;)
            {
                var line = words[i].Trim();
                if(line.Length > 7)
                {
                    line = line.Replace("  ", " ");
                    line = line.ToLower();
                    line = char.ToUpper(line[0]) + line[1..];
                    words[i] = line + ".";
                    i++;
                }
                else words.RemoveAt(i);
            }

            return words;
        }
    }
}
