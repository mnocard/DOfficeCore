using System.Collections.Generic;
using System.Collections.ObjectModel;

using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    interface IDataProviderService
    {
        bool SaveDataToFile<T>(IEnumerable<T> data, string path);
        List<Section> LoadDataFromFile(string path);
        ObservableCollection<Sector> LoadSectorsFromFile(string path);
    }
}