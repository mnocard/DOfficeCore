using System.Collections.Generic;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string fileName);
        IEnumerable<string> LoadDoctorsFromFile(string fileName);
        HashSet<Section> LoadDataFromFile(string fileName);
    }
}