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
            if (data == null) return false;
            else if (string.IsNullOrEmpty(fileName)) return false;

            try
            {
                var json = JsonSerializer.Serialize(data);
                File.WriteAllText(fileName + ".json", json, Encoding.UTF8);
            }

            catch (Exception)
            {
                Log.Error($"Can't save file. Error.");
                throw;
            }

            Log.Information("File saved succesfully");
            return true;
        }

        /// <summary>
        /// Загрузка списка докторов из файла (применимо к должностям
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Возвращаемый список докторов или долдностей</returns>
        public IEnumerable<string> LoadDoctorsFromFile(string fileName)
        {

            if (string.IsNullOrEmpty(fileName)) return new List<string>();

            try
            {
                if (!File.Exists(fileName + ".json"))
                {
                    using FileStream fs = File.Create(fileName + ".json");

                    Log.Verbose($"File {fileName} doesn't exist");

                    return new List<string>();
                }
                else
                {
                    var jsonString = File.ReadAllText(fileName + ".json");
                    if (!String.IsNullOrEmpty(jsonString))
                        return JsonSerializer.Deserialize<IEnumerable<string>>(jsonString);
                }
            }
            catch (Exception)
            {
                Log.Error($"Can't load doctors from file {fileName}.json. Error.");
                throw;
            }
            return new List<string>();
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция данных</returns>
        public List<Section> LoadDataFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return new List<Section>();
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

                    if (!String.IsNullOrEmpty(jsonString))
                        return JsonSerializer.Deserialize<List<Section>>(jsonString);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Can't load data from file {fileName}.json. Error.");
                throw e;
            }
            return new List<Section>();
        }
    }
}
