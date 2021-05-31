using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Serilog;

namespace DOfficeCore.Services
{
    /// <summary>Класс, реализующий сохранение данных в файл и загрузки данных из файла</summary>
    internal class DataProviderService : IDataProviderService
    {
        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        /// <typeparam name="T">Тип сохраняемого списки</typeparam>
        /// <param name="data">Собственно список сохраняемых данных</param>
        /// <param name="fileName">Имя в файла, который будет происходить запись данных</param>
        /// <returns></returns>
        public bool SaveDataToFile<T>(IEnumerable<T> data, string fileName)
        {
            if (data is null) return false;
            else if (string.IsNullOrEmpty(fileName)) return false;

            try
            {
                var json = JsonSerializer.Serialize(data);
                File.WriteAllText(fileName + ".json", json, Encoding.UTF8);
            }

            catch (Exception e)
            {
                Log.Error($"Can't save file. Error:\n{0}", e.Message);
                throw new Exception("Unexpected error", e);
            }

            Log.Information("File saved succesfully");
            return true;
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция данных</returns>
        public List<Section> LoadDataFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return new List<Section>();

            var result = new List<Section>();
            try
            {
                if (!File.Exists(fileName + ".json"))
                {
                    using FileStream fs = File.Create(fileName + ".json");
                    Log.Verbose($"File {fileName} doesn't exist");
                    return new List<Section>();
                }
                else
                {
                    var jsonString = File.ReadAllText(fileName + ".json");

                    if (!string.IsNullOrEmpty(jsonString))
                        result.AddRange(JsonSerializer.Deserialize<List<Section>>(jsonString));
                }
            }
            catch (Exception e)
            {
                Log.Error($"Can't load data from file {0}.json. Error:\n{1}.", fileName, e.Message);
                throw new Exception("Unexpected error", e);
            }
            return result;
        }
    }
}
