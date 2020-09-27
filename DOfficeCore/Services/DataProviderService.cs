using DOfficeCore.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace DOfficeCore.Services
{
    static class DataProviderService
    {

        public static bool SaveDataToFile(IEnumerable data, string fileName)
        {
            bool result = false;
            using (StreamWriter file = File.CreateText(fileName + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
                result = true;
            }
            return result;
        }

        public static ObservableCollection<Diagnosis> LoadDataFromFile(string fileName)
        {
            return JsonConvert.DeserializeObject<ObservableCollection<Diagnosis>>(fileName);
        }
    }
}
