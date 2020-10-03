using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        void DiagnosisFromDataToView(ViewCollection viewCollection);
        void BlocksFromDataToView(ViewCollection viewCollection);
        void LinesFromDataToView(ViewCollection viewCollection);
    }
}