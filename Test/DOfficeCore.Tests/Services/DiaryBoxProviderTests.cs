﻿using DOfficeCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOfficeCore.Tests.Services
{
    [TestClass]
    public class DiaryBoxProviderTests
    {
        #region DocToDiary

        [TestMethod]
        public void DocToDiary_with_full_args()
        {
            const string DiaryBox = "Дневник";
            const string position = "Заведующий отделением";
            const string doctor = "Мамбетов Х.С.";

            const string expected_result = DiaryBox + "\n" + position + "\t\t\t" + doctor;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_null_DiaryBox()
        {
            const string DiaryBox = null;
            const string position = "Заведующий отделением";
            const string doctor = "Мамбетов Х.С.";

            const string expected_result = "" + "\n" + position + "\t\t\t" + doctor;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_StringEmpty_DiaryBox()
        {
            string DiaryBox = string.Empty;
            const string position = "Заведующий отделением";
            const string doctor = "Мамбетов Х.С.";

            const string expected_result = "" + "\n" + position + "\t\t\t" + doctor;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_null_Position()
        {
            const string DiaryBox = "Дневник";
            const string position = null;
            const string doctor = "Мамбетов Х.С.";

            const string expected_result = DiaryBox;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_StringEmpty_Position()
        {
            const string DiaryBox = "Дневник";
            string position = string.Empty;
            const string doctor = "Мамбетов Х.С.";

            const string expected_result = DiaryBox;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_null_Doctor()
        {
            const string DiaryBox = "Дневник";
            const string position = "Заведующий отделением";
            const string doctor = null;

            const string expected_result = DiaryBox;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_StringEmpty_Doctor()
        {
            const string DiaryBox = "Дневник";
            const string position = "Заведующий отделением";
            string doctor = string.Empty;

            const string expected_result = DiaryBox;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DocToDiary_with_Contains_elements_in_DiaryBox()
        {
            const string DiaryBox = "Дневник\nЗаведующий отделением\t\t\tМамбетов Х.С.";
            const string position = "Заведующий отделением";
            const string doctor = "Мамбетов Х.С.";

            const string expected_result = "Дневник";

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.DocToDiary(DiaryBox, position, doctor);

            Assert.AreEqual(expected_result, actualResult);
        }
        #endregion

        #region LineToDiaryBox
        
        [TestMethod]
        public void LineToDiaryBox_full_args()
        {
            const string DiaryBox = "Дневник. ";
            const string Line = "Новая строка.";

            const string expected_result = DiaryBox + Line + " ";

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_Contains_elements_in_DiaryBox()
        {
            const string DiaryBox = "Дневник. Новая строка. ";
            const string Line = "Новая строка.";

            const string expected_result = "Дневник. ";

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_null_DiaryBox()
        {
            const string DiaryBox = null;
            const string Line = "Новая строка.";

            const string expected_result = "" + Line + " ";

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_stringEmpty_DiaryBox()
        {
            string DiaryBox = string.Empty;
            const string Line = "Новая строка.";

            const string expected_result = "" + Line + " ";

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_null_Line()
        {
            const string DiaryBox = "Дневник";
            const string Line = null;

            const string expected_result = DiaryBox;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_stringEmpty_Line()
        {
            const string DiaryBox = "Дневник";
            string Line = string.Empty;

            const string expected_result = DiaryBox;

            var logger = new Logger.Logger();
            var d = new DiaryBoxProvider(logger);

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        #endregion
    }
}
