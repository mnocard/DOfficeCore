using System.Collections.Generic;
using System.Text;
using System.Xml;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
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

        }
    }
}
