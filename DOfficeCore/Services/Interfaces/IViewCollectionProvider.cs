using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    [Obsolete("Класс устарел. Используй INewViewCollectionProvider", true)]
    interface IViewCollectionProvider
    {
        ObservableCollection<Section> DiagnosisFromDataToView(IEnumerable<Section> DataCollection);
        ObservableCollection<Section> BlocksFromDataToView(IEnumerable<Section> DataCollection, Section CurrentSection);
        ObservableCollection<Section> LinesFromDataToView(IEnumerable<Section> DataCollection, Section CurrentSection);

        ObservableCollection<Section> SearchDiagnosis(List<Section> DataCollection, string MultiBox);
        ObservableCollection<Section> SearchBlocks(List<Section> DataCollection, string MultiBox);
        ObservableCollection<Section> SearchLines(List<Section> DataCollection, string MultiBox);
    }
}