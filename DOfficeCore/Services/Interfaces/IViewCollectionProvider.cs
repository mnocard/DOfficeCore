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

        bool RemoveDiagnosis(HashSet<Section> DataCollection, Section CurrentSection);
        bool RemoveBlock(HashSet<Section> DataCollection, Section CurrentSection);
        bool RemoveLine(HashSet<Section> DataCollection, Section CurrentSection);

        bool EditDiagnosis(HashSet<Section> DataCollection, Section CurrentItem, string MultiBox);
        bool EditBlock(HashSet<Section> DataCollection, Section CurrentItem, string MultiBox);
        bool EditLine(HashSet<Section> DataCollection, Section CurrentItem, string MultiBox);

        ObservableCollection<Section> SearchDiagnosis(HashSet<Section> DataCollection, string MultiBox);
        ObservableCollection<Section> SearchBlocks(HashSet<Section> DataCollection, string MultiBox);
        ObservableCollection<Section> SearchLines(HashSet<Section> DataCollection, string MultiBox);

        bool AddDiagnosis(HashSet<Section> DataCollection, string MultiBox);
        bool AddBlock(HashSet<Section> DataCollection, Section CurrentSection, string MultiBox);
        bool AddLine(HashSet<Section> DataCollection, Section CurrentSection, string MultiBox);
        
        string RandomDiary(HashSet<Section> DataCollection, Section CurrentSection);
    }
}