using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

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
                throw;
            }

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
                    return new List<string>();
                }
                else
                {
                    var jsonString = File.ReadAllText(fileName + ".json");
                    IEnumerable<string> result = JsonSerializer.Deserialize<IEnumerable<string>>(jsonString);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция данных</returns>
        public List<Section> LoadDataFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return new List<Section>();
            List<Section> result;
            try
            {
                if (!File.Exists(fileName + ".json"))
                {
                    using FileStream fs = File.Create(fileName + ".json");
                    result = new List<Section>();
                }
                else
                {
                    var jsonString = File.ReadAllText(fileName + ".json");
                    result = JsonSerializer.Deserialize<List<Section>>(jsonString);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
