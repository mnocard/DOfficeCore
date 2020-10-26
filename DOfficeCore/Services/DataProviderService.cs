﻿using DOfficeCore.Logger;
using DOfficeCore.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace DOfficeCore.Services
{
    /// <summary>Класс, реализующий сохранение данных в файл и загрузки данных из файла</summary>
    internal class DataProviderService : IDataProviderService
    {
        private readonly ILogger _Logger;

        public DataProviderService(ILogger Logger) => _Logger = Logger;

        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        /// <typeparam name="T">Тип сохраняемого списки</typeparam>
        /// <param name="data">Собственно список сохраняемых данных</param>
        /// <param name="fileName">Имя в файла, который будет происходить запись данных</param>
        /// <returns></returns>
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
            if(result) _Logger.WriteLog("File saved succesfully");
            else _Logger.WriteLog("ERROR! File save filed");

            return result;
        }

        /// <summary>
        /// Загрузка списка докторов из файла (применимо к должностям
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Возвращаемый список докторов или долдностей</returns>
        public IEnumerable<string> LoadDoctorsFromFile(string fileName)
        {
            _Logger.WriteLog("INFO");

            if (!File.Exists(fileName))
            {
                using FileStream fs = File.Create(fileName);
                
                _Logger.WriteLog($"File {fileName} doesn't exist");

                return new HashSet<string>();
            }
            else
            {
                using StreamReader file = File.OpenText(fileName);
                JsonSerializer serializer = new JsonSerializer();
                var result = (IEnumerable<string>)serializer.Deserialize(file, typeof(IEnumerable<string>));
                
                _Logger.WriteLog("File loaded succesfully");

                return result;
            }
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция данных</returns>
        public HashSet<Section> LoadDataFromFile(string fileName)
        {
            _Logger.WriteLog("INFO");

            HashSet<Section> result = null;

            if (!File.Exists(fileName + ".json"))
            {
                using FileStream fs = File.Create(fileName + ".json");
                result = new HashSet<Section>();
                _Logger.WriteLog($"File {fileName} doesn't exist");
            }
            else
            {
                using StreamReader file = File.OpenText(fileName + ".json");
                JsonSerializer serializer = new JsonSerializer();
                result = (HashSet<Section>)serializer.Deserialize(file, typeof(HashSet<Section>));
                _Logger.WriteLog("File loaded succesfully");
            }

            return result;
        }
    }
}
