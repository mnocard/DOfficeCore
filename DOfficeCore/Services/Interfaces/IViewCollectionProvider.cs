using DOfficeCore.Models;

namespace DOfficeCore.Services
{
     interface IViewCollectionProvider
    {
        ViewCollection DiagnosisFromDataToView(ViewCollection viewCollection);
        ViewCollection BlocksFromDataToView(ViewCollection viewCollection, string code);
        ViewCollection LinesFromDataToView(ViewCollection viewCollection, string code, string block);
    }
}