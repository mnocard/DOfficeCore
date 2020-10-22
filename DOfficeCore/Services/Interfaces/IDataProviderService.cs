using DOfficeCore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string fileName);
        IEnumerable<string> LoadDoctorsFromFile(string fileName);
        ObservableCollection<Section> LoadDataFromFile(string fileName);
    }
}