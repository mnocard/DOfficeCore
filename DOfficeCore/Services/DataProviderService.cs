using DOfficeCore.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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

            using (var file = File.CreateText(fileName + ".json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, data);
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

            if (!File.Exists(fileName + ".json"))
            {
                using var fs = File.Create(fileName + ".json");
                return new List<string>();
            }
            else
            {
                using var file = File.OpenText(fileName + ".json");
                var serializer = new JsonSerializer();
                return (IEnumerable<string>)serializer.Deserialize(file, typeof(IEnumerable<string>));
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

            if (!File.Exists(fileName + ".json"))
            {
                using var fs = File.Create(fileName + ".json");
                return new List<Section>();
            }
            else
            {
                using var file = File.OpenText(fileName + ".json");
                var serializer = new JsonSerializer();
                return (List<Section>)serializer.Deserialize(file, typeof(List<Section>));
            }
        }
    }
}
