﻿using DOfficeCore.Logger;
using DOfficeCore.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
            if (data == null)
            {
                _Logger.WriteLog("Collection is null");
                return false;
            } 
            else if (string.IsNullOrEmpty(fileName))
            {
                _Logger.WriteLog("Filename can't be empty");
                return false;
            }

            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName + ".json", json);
            
            _Logger.WriteLog("File saved succesfully");
            return true;
        }

        /// <summary>
        /// Загрузка списка докторов из файла (применимо к должностям
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Возвращаемый список докторов или долдностей</returns>
        public IEnumerable<string> LoadDoctorsFromFile(string fileName)
        {
            _Logger.WriteLog("INFO");

            if (string.IsNullOrEmpty(fileName))
            {
                _Logger.WriteLog("Filename can't be empty");
                return new List<string>();
            }

            if (!File.Exists(fileName + ".json"))
            {
                using FileStream fs = File.Create(fileName + ".json");
                
                _Logger.WriteLog($"File {fileName} doesn't exist");

                return new List<string>();
            }
            else
            {
                var jsonString = File.ReadAllText(fileName + ".json");

                IEnumerable<string> result = JsonSerializer.Deserialize<IEnumerable<string>>(jsonString);
                
                _Logger.WriteLog("File loaded succesfully");

                return result;
            }
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция данных</returns>
        public List<Section> LoadDataFromFile(string fileName)
        {
            _Logger.WriteLog("INFO");

            List<Section> result = null;
            if (string.IsNullOrEmpty(fileName))
            {
                _Logger.WriteLog("Filename can't be empty");
                return new List<Section>();
            }

            if (!File.Exists(fileName + ".json"))
            {
                using FileStream fs = File.Create(fileName + ".json");
                result = new List<Section>();
                _Logger.WriteLog($"File {fileName} doesn't exist");
            }
            else
            {
                var jsonString = File.ReadAllText(fileName + ".json");

                result = JsonSerializer.Deserialize<List<Section>>(jsonString);

                _Logger.WriteLog("File loaded succesfully");
            }

            return result;
        }
    }
}
