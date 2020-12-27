using System;
using DOfficeCore.Services;
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

            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

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


            var d = new DiaryBoxProvider();

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_Contains_elements_in_DiaryBox()
        {
            const string DiaryBox = "Дневник. Новая строка. ";
            const string Line = "Новая строка.";

            const string expected_result = "Дневник. ";


            var d = new DiaryBoxProvider();

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_null_DiaryBox()
        {
            const string DiaryBox = null;
            const string Line = "Новая строка.";

            const string expected_result = "" + Line + " ";


            var d = new DiaryBoxProvider();

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_stringEmpty_DiaryBox()
        {
            string DiaryBox = string.Empty;
            const string Line = "Новая строка.";

            const string expected_result = "" + Line + " ";


            var d = new DiaryBoxProvider();

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_null_Line()
        {
            const string DiaryBox = "Дневник";
            const string Line = null;

            const string expected_result = DiaryBox;


            var d = new DiaryBoxProvider();

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void LineToDiaryBox_stringEmpty_Line()
        {
            const string DiaryBox = "Дневник";
            string Line = string.Empty;

            const string expected_result = DiaryBox;


            var d = new DiaryBoxProvider();

            var actualResult = d.LineToDiaryBox(DiaryBox, Line);

            Assert.AreEqual(expected_result, actualResult);
        }

        #endregion

        #region DateToDiaryBox
        [TestMethod]
        public void DateToDiaryBox_full_args()
        {
            const string DiaryBox = "Дневник. ";
            DateTime ChoosenDate = DateTime.Now;

            string expected_result = DiaryBox.Insert(0, ChoosenDate.ToString("dd.MM.yyyy") + "\n");


            var d = new DiaryBoxProvider();

            var actualResult = d.DateToDiaryBox(DiaryBox, ChoosenDate);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DateToDiaryBox_Contains_date_in_DiaryBox()
        {
            DateTime ChoosenDate = DateTime.Now;
            string DiaryBox = ChoosenDate.ToString("dd.MM.yyyy") + "\n" + "Дневник. ";

            const string expected_result = "Дневник. ";


            var d = new DiaryBoxProvider();

            var actualResult = d.DateToDiaryBox(DiaryBox, ChoosenDate);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DateToDiaryBox_null_DiaryBox()
        {
            const string DiaryBox = null;
            DateTime ChoosenDate = DateTime.Now;

            string expected_result = ChoosenDate.ToString("dd.MM.yyyy") + "\n";


            var d = new DiaryBoxProvider();

            var actualResult = d.DateToDiaryBox(DiaryBox, ChoosenDate);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DateToDiaryBox_stringEmpty_DiaryBox()
        {
            string DiaryBox = string.Empty;
            DateTime ChoosenDate = DateTime.Now;

            string expected_result = ChoosenDate.ToString("dd.MM.yyyy") + "\n";


            var d = new DiaryBoxProvider();

            var actualResult = d.DateToDiaryBox(DiaryBox, ChoosenDate);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void DateToDiaryBox_another_ChoosenDate()
        {
            string DiaryBox = "01.01.2020\nДневник. ";
            DateTime ChoosenDate = DateTime.Now;

            const string expected_result = "Дневник. ";


            var d = new DiaryBoxProvider();

            var actualResult = d.DateToDiaryBox(DiaryBox, ChoosenDate);

            Assert.AreEqual(expected_result, actualResult);
        }

        #endregion

        #region TimeToDiaryBox

        [TestMethod]
        public void TimeToDiaryBox_full_args_without_Date()
        {
            const string DiaryBox = "Дневник. ";
            DateTime ChoosenTime = DateTime.Now;

            string expected_result = DiaryBox.Insert(0, ChoosenTime.ToString("dd.MM.yyyy HH:mm") + "\n");


            var d = new DiaryBoxProvider();

            var actualResult = d.TimeToDiaryBox(DiaryBox, ChoosenTime);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void TimeToDiaryBox_full_args_with_Date()
        {
            DateTime ChoosenTime = DateTime.Now;
            string DiaryBox = ChoosenTime.ToString("dd.MM.yyyy") + "\nДневник. ";

            string expected_result = DiaryBox.Replace(ChoosenTime.ToString("dd.MM.yyyy"), ChoosenTime.ToString("dd.MM.yyyy HH:mm"));


            var d = new DiaryBoxProvider();

            var actualResult = d.TimeToDiaryBox(DiaryBox, ChoosenTime);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void TimeToDiaryBox_Contains_time_in_DiaryBox()
        {
            DateTime ChoosenTime = DateTime.Now;
            string DiaryBox = ChoosenTime.ToString("dd.MM.yyyy HH:mm") + "\nДневник. ";

            string expected_result = ChoosenTime.ToString("dd.MM.yyyy") + "\nДневник. ";


            var d = new DiaryBoxProvider();

            var actualResult = d.TimeToDiaryBox(DiaryBox, ChoosenTime);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void TimeToDiaryBox_null_DiaryBox()
        {
            const string DiaryBox = null;
            DateTime ChoosenTime = DateTime.Now;

            string expected_result = ChoosenTime.ToString("dd.MM.yyyy HH:mm") + "\n";


            var d = new DiaryBoxProvider();

            var actualResult = d.TimeToDiaryBox(DiaryBox, ChoosenTime);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void TimeToDiaryBox_stringEmpty_DiaryBox()
        {
            string DiaryBox = string.Empty;
            DateTime ChoosenTime = DateTime.Now;

            string expected_result = ChoosenTime.ToString("dd.MM.yyyy HH:mm") + "\n";


            var d = new DiaryBoxProvider();

            var actualResult = d.TimeToDiaryBox(DiaryBox, ChoosenTime);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void TimeToDiaryBox_another_ChoosenTime()
        {
            DateTime ChoosenTime = DateTime.Now;
            string DiaryBox = "01.01.2020 10:00\nДневник. ";

            string expected_result = ChoosenTime.ToString("dd.MM.yyyy") + "\nДневник. ";


            var d = new DiaryBoxProvider();

            var actualResult = d.TimeToDiaryBox(DiaryBox, ChoosenTime);

            Assert.AreEqual(expected_result, actualResult);
        }

        #endregion
    }
}
