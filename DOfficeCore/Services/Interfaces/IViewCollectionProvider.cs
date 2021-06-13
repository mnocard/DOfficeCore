using System.Collections.Generic;
using System.Collections.ObjectModel;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    interface IViewCollectionProvider
    {
        ObservableCollection<Section> DiagnosisFromDataToView(IEnumerable<Section> DataCollection);
        ObservableCollection<Section> BlocksFromDataToView(IEnumerable<Section> DataCollection, Section CurrentSection);
        ObservableCollection<Section> LinesFromDataToView(IEnumerable<Section> DataCollection, Section CurrentSection);

        bool RemoveDiagnosis(List<Section> DataCollection, Section CurrentSection);
        bool RemoveBlock(List<Section> DataCollection, Section CurrentSection);
        bool RemoveLine(List<Section> DataCollection, Section CurrentSection);

        bool EditDiagnosis(List<Section> DataCollection, Section CurrentItem, string MultiBox);
        bool EditBlock(List<Section> DataCollection, Section CurrentItem, string MultiBox);
        bool EditLine(List<Section> DataCollection, Section CurrentItem, string MultiBox);

        ObservableCollection<Section> SearchDiagnosis(List<Section> DataCollection, string MultiBox);
        ObservableCollection<Section> SearchBlocks(List<Section> DataCollection, string MultiBox);
        ObservableCollection<Section> SearchLines(List<Section> DataCollection, string MultiBox);

        Section AddDiagnosis(List<Section> DataCollection, string MultiBox);
        Section AddBlock(List<Section> DataCollection, Section CurrentSection, string MultiBox);
        Section AddLine(List<Section> DataCollection, Section CurrentSection, string MultiBox);

        (string, ObservableCollection<Section>) RandomDiary(List<Section> DataCollection, Section CurrentSection);
    }
}