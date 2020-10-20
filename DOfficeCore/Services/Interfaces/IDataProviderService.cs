using DOfficeCore.Models;
using System.Collections.Generic;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string fileName);
        IEnumerable<string> LoadDoctorsFromFile(string fileName);
        HashSet<Diagnosis> LoadDataFromFile(string fileName);
    }
}