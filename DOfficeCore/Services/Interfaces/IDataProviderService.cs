using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    /// <summary>Сервис сохранения данных в файл и загрузки данных из json-файла</summary>
    interface IDataProviderService
    {
        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        /// <typeparam name="T">Тип сохраняемого списки</typeparam>
        /// <param name="data">Собственно список сохраняемых данных</param>
        /// <param name="fileName">Имя в файла, который будет происходить запись данных</param>
        /// <returns></returns>
        bool SaveDataToFile<T>(IEnumerable<T> data, string path);

        /// <summary>
        /// Загрузка списка секторов из json-файла
        /// </summary>
        /// <param name="path">Путь к файлу с полным именем файла</param>
        /// <returns>Список полученных секторов</returns>
        List<Sector> LoadSectorsFromJson(string path);
    }
}