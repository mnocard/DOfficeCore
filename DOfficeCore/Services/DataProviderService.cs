using DOfficeCore.Logger;
using DOfficeCore.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DOfficeCore.Services
{
    /// <summary>Класс, реализующий сохранение данных в файл и загрузки данных из файла</summary>
    internal class DataProviderService : IDataProviderService
    {
        private readonly ILogger _Logger;

        public DataProviderService(ILogger Logger) => _Logger = Logger;
        public bool SaveDataToFile<T>(IEnumerable<T> data, string fileName)
        {
            _Logger.WriteLog("INFO");

            bool result = false;
            using (StreamWriter file = File.CreateText(fileName + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
                result = true;
            }
            
            _Logger.WriteLog("DONE");
            
            return result;
        }

        public IEnumerable<string> LoadDoctorsFromFile(string fileName)
        {
            _Logger.WriteLog("INFO");

            if (!File.Exists(fileName))
            {
                using FileStream fs = File.Create(fileName);
                
                _Logger.WriteLog("DONE");

                return null;
            }
            else
            {
                using StreamReader file = File.OpenText(fileName);
                JsonSerializer serializer = new JsonSerializer();
                var result = (IEnumerable<string>)serializer.Deserialize(file, typeof(IEnumerable<string>));
                
                _Logger.WriteLog("DONE");

                return result;
            }
        }

        public List<Diagnosis> LoadDataFromFile(string fileName)
        {
            _Logger.WriteLog("INFO");

            List<Diagnosis> result = null;

            if (!File.Exists(fileName))
            {
                using FileStream fs = File.Create(fileName);
            }
            else
            {
                using StreamReader file = File.OpenText(fileName);
                JsonSerializer serializer = new JsonSerializer();
                result = (List<Diagnosis>)serializer.Deserialize(file, typeof(List<Diagnosis>));
            }
            _Logger.WriteLog("DONE");

            return result;
        }
    }
}
