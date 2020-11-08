using System;
using DOfficeCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace DOfficeCore.Tests.Services
{
    [TestClass]
    public class LineEditorServiceTests
    {
        #region OpenDocument

        [TestMethod]
        public void OpenDocument_full_args_Docx()
        {
            var d = new LineEditorService();

            var actualResult = d.OpenDocument("example.docx");

            Assert.IsTrue(actualResult.Length != 0);
        }

        [TestMethod]
        public void OpenDocument_with_null()
        {
            var d = new LineEditorService();

            var actualResult = d.OpenDocument(null);

        }

        [TestMethod]
        public void OpenDocument_with_wrong_ext()
        {
            var d = new LineEditorService();

            var actualResult = d.OpenDocument("lines.json");

            Assert.IsFalse(actualResult.Length != 0);
        }

        #endregion
    }
}

