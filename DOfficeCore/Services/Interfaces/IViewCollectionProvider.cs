using System.Collections.Generic;
using System.Collections.ObjectModel;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        string RandomDiary(string CurrentDiagnosis, HashSet<Diagnosis> DataCollection);
        ObservableCollection<string> DiagnosisFromDataToView(HashSet<Diagnosis> DataCollection);
        ObservableCollection<string> BlocksFromDataToView(string CurrentDiagnosis, HashSet<Diagnosis> DataCollection);
        ObservableCollection<string> LinesFromDataToView(HashSet<Diagnosis> DataCollection, string CurrentDiagnosis, string CurrentBlock);

        public ObservableCollection<string> SearchDiagnosis(string MultiBox, HashSet<Diagnosis> DataCollection);
        public ObservableCollection<string> SearchBlocks(string MultiBox, HashSet<Diagnosis> DataCollection);
        public ObservableCollection<string> SearchLines(string MultiBox, HashSet<Diagnosis> DataCollection);

        HashSet<Diagnosis> RemoveElement(
            string FocusedDataGrid,
            string MultiBox,
            HashSet<Diagnosis> DataCollection,
            string CurrentDiagnosis,
            string CurrentBlock,
            string CurrentLine);
        bool EditDiagnosis(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem);
        bool EditBlock(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem);
        bool EditLine(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem);
        void SelectedData(string FocusedDataGrid, string CurrentItem);
        void AddELement(string FocusedDataGrid, string MultiBox);
    }
}