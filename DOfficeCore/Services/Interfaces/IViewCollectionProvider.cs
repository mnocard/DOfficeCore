using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        void DiagnosisFromDataToView();
        void BlocksFromDataToView();
        void LinesFromDataToView();
        void RemoveElement(string FocusedDataGrid, string MultiBox);
        void SearchElement(string MultiBox);
        void EditElement(string FocusedDataGrid, string MultiBox);
        void SelectedData(string FocusedDataGrid, string CurrentItem);
    }
}