using DOfficeCore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string fileName);

        ObservableCollection<Diagnosis> LoadDataFromFile(string fileName);
    }
}