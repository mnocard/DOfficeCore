using System.Collections.Generic;
using DOfficeCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOfficeCore.Tests.Services
{
    [TestClass]
    public class DataProviderServiceTests
    {

        [TestMethod]
        public void SaveDataToFile_IEnumerable_to_Json_with_FileName()
        {
            var list = new List<string>()
            {
                "first", "second", "third"
            };
            const string fileName = "testFile";

            const bool expected_result = true;

            var logger = new Logger.Logger();
            var d = new DataProviderService(logger);
            
            var actualResult = d.SaveDataToFile(list, fileName);

            Assert.AreEqual(expected_result, actualResult);
        }

        [TestMethod]
        public void SaveDataToFile_Null_to_Json_with_FileName()
        {
            List<string> list = null;
            const string fileName = "testFile";
            const bool expected_result = false;

            var logger = new Logger.Logger();
            var d = new DataProviderService(logger);

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
            const string fileName = "";
            const bool expected_result = false;

            var logger = new Logger.Logger();
            var d = new DataProviderService(logger);

            var actualResult = d.SaveDataToFile(list, fileName);

            Assert.AreEqual(expected_result, actualResult);
        }
    }
}
