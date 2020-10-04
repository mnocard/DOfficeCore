using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        void DiagnosisFromDataToView();
        void BlocksFromDataToView();
        void LinesFromDataToView();
        void SearchElement(string MultiBox);
        void RemoveElement(string FocusedDataGrid, string MultiBox);
        void EditElement(string FocusedDataGrid, string MultiBox);
        void SelectedData(string FocusedDataGrid, string CurrentItem);
    }
}