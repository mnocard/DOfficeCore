using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        ViewCollection DiagnosisFromDataToView(ViewCollection viewCollection);
        ViewCollection BlocksFromDataToView(ViewCollection viewCollection);
        ViewCollection LinesFromDataToView(ViewCollection viewCollection);
    }
}