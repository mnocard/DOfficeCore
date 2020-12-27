using System;
using System.Collections.Generic;
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
        [ExpectedException(typeof(Exception))]
        public void OpenDocument_with_wrong_ext()
        {


            var d = new LineEditorService();

            const string filePath = "lines.json";

            var actualResult = d.OpenDocument(filePath);
        }

        #endregion

        #region TextToLines

        [TestMethod]
        public void TextToLines_full_args()
        {


            var d = new LineEditorService();
            var expectedResult = new List<string>()
            {
                "Соматическое состояние: жалоб нет.",
                "Рост 162 см.",
                ", вес 57 кг.",
                "Кожные покровы бледные, видимые слизистые оболочки чистые, влажные, розовой окраски.",
                "На левом предплечье два шрама от самопореза.",
                "В легких дыхание везикулярное, хрипов нет.",
                "Тоны сердца ясные, ритмичные, шумов нет, ад 105/80 мм.",
                "Живот при пальпации мягкий, безболезненный.",
                "Печень и селезенка не увеличены.",
                "Симптом поколачивания в области почек отрицательный с обеих сторон."
            };

            const string line = "СОМАТИЧЕСКОЕ СОСТОЯНИЕ: жалоб нет. Рост 162 см., вес 57 кг. Кожные покровы бледные, видимые слизистые оболочки чистые, влажные, розовой окраски. На левом предплечье два шрама от самопореза. В легких дыхание везикулярное, хрипов нет. Тоны сердца ясные, ритмичные, шумов нет, АД 105/80 мм. рт. ст. Живот при пальпации мягкий, безболезненный. Печень и селезенка не увеличены. Симптом поколачивания в области почек отрицательный с обеих сторон.";

            var actualResult = d.TextToLines(line);

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TextToLines_with_null()
        {


            var d = new LineEditorService();

            const string line = null;

            var actualResult = d.TextToLines(line);
        }

        #endregion
    }
}

