using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        void DiagnosisFromDataToView();
        void BlocksFromDataToView();
        void LinesFromDataToView();
    }
}