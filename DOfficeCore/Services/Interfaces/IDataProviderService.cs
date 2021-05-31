using System.Collections.Generic;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string fileName);
        List<Section> LoadDataFromFile(string fileName);
    }
}