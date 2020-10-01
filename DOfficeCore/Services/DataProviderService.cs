using DOfficeCore.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace DOfficeCore.Services
{
    static class DataProviderService
    {

        public static bool SaveDataToFile<T>(IEnumerable<T> data, string fileName)
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
            ObservableCollection<Diagnosis> result = null;

            if (!File.Exists(fileName))
            {
                using FileStream fs = File.Create(fileName);
            }
            else
            {
                using (StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = (ObservableCollection<Diagnosis>)serializer.Deserialize(file, typeof(ObservableCollection<Diagnosis>));
                }
            }

            return result;
        }
    }
}
