using System.Collections.Generic;
using DOfficeCore.Logger;
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
    }
}
