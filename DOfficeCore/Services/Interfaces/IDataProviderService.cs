using System;
using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string path);

        [Obsolete("Метод устарел. Используй LoadSectorsFromFile", true)]
        List<Section> LoadDataFromFile(string path);
        List<Sector> LoadSectorsFromFile(string path);
    }
}