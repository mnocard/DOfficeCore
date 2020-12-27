using System.Collections.Generic;
using System.Linq;
using DOfficeCore.Models;
using DOfficeCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOfficeCore.Tests.Services
{
    [TestClass]
    public class DataProviderServiceTests
    {
        #region SaveDataToFile

        [TestMethod]
        public void SaveAndLoadDoctors()
        {
            const string fileName = "SaveAndLoad";
            var list = new List<string>()
            {
                "first", "second", "third"
            };
            var d = new DataProviderService();

            d.SaveDataToFile(list, fileName);

            var result = new List<string>(d.LoadDoctorsFromFile(fileName));

            CollectionAssert.AreEqual(list, result);
        }

        [TestMethod]
        public void SaveDataToFile_Null_to_Json_with_FileName()
        {
            List<string> list = null;
            const string fileName = "testFile";
            const bool expected_result = false;

            var d = new DataProviderService();

            var actualResult = d.SaveDataToFile(list, fileName);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void SaveDataToFile_IEnumerable_to_Json_with_FileNameNull()
        {
            var list = new List<string>()
            {
                "first", "second", "third"
            };
            const string fileName = null;
            const bool expected_result = false;

            var d = new DataProviderService();

            var actualResult = d.SaveDataToFile(list, fileName);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void SaveDataToFile_IEnumerable_to_Json_with_FileNameStringEmpty()
        {
            var list = new List<string>()
            {
                "first", "second", "third"
            };

            string fileName = string.Empty;
            const bool expected_result = false;

            var d = new DataProviderService();

            var actualResult = d.SaveDataToFile(list, fileName);

            Assert.AreEqual(expected_result, actualResult);
        }

        #endregion

        #region LoadDoctorsFromFile

        [TestMethod]
        public void LoadDoctorsFromFile_IEnumerable_with_FileNameNull()
        {
            const string fileName = null;
            var d = new DataProviderService();
            var expectedResult = new List<string>();

            var actualResult = new List<string>(d.LoadDoctorsFromFile(fileName));

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void LoadDoctorsFromFile_IEnumerable_with_FileNameStringEmpty()
        {
            string fileName = string.Empty;
            var d = new DataProviderService();
            var expectedResult = new List<string>();

            var actualResult = new List<string>(d.LoadDoctorsFromFile(fileName));

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        #endregion

        #region LoadDataFromFile

        [TestMethod]
        public void SaveAndLoadData()
        {
            const string fileName = "lines";

            var d = new DataProviderService();
            var expectedResult = new List<Section>()
            {
                new Section()
                {
                    Diagnosis = "123",
                    Block = "123",
                    Line = "123"
                },
                new Section()
                {
                    Diagnosis = "123 123",
                    Block = "123 123",
                    Line = "123"
                }
            };

            d.SaveDataToFile(expectedResult, fileName);

            var actualResult = d.LoadDataFromFile(fileName);

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void LoadDataFromFile_IEnumerable_with_FileNameStringEmpty()
        {
            string fileName = string.Empty;

            var d = new DataProviderService();

            var actualResult = d.LoadDataFromFile(fileName);

            Assert.IsTrue(actualResult.Count() == 0);
        }

        [TestMethod]
        public void LoadDataFromFile_IEnumerable_with_FileNameNull()
        {
            const string fileName = null;

            var d = new DataProviderService();

            var actualResult = d.LoadDataFromFile(fileName);

            Assert.IsTrue(actualResult.Count() == 0);
        }
        #endregion
    }
}
