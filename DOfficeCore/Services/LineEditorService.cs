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

        public string OpenDocument(string filepath)
        {
            const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

            var textBuilder = new StringBuilder();

            using (var wdDoc = WordprocessingDocument.Open(filepath, false))
            {
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
            }
            return textBuilder.ToString();
        }

        public List<string> TextToLines(string lines)
        {
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
            return words;
        }
    }
}
