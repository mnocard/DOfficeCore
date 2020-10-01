﻿using DOfficeCore.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace DOfficeCore.Services
{
    /// <summary>Класс, реализующий сохранение данных в файл и загрузки данных из файла</summary>
    internal class DataProviderService : IDataProviderService
    {
        public bool SaveDataToFile<T>(IEnumerable<T> data, string fileName)
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

        public IEnumerable<Diagnosis> LoadDataFromFile(string fileName)
        {
            IEnumerable<Diagnosis> result = null;

            if (!File.Exists(fileName))
            {
                using FileStream fs = File.Create(fileName);
            }
            else
            {
                using (StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = (IEnumerable<Diagnosis>)serializer.Deserialize(file, typeof(IEnumerable<Diagnosis>));
                }
            }

            return result;
        }
    }
}
