using System;
using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    [Obsolete("Класс устарел. Используй INewCollectionHandler", true)]
    public interface ICollectionHandler
    {
        bool RemoveDiagnosis(List<Section> DataCollection, Section CurrentSection);
        bool RemoveBlock(List<Section> DataCollection, Section CurrentSection);
        bool RemoveLine(List<Section> DataCollection, Section CurrentSection);

        bool EditDiagnosis(List<Section> DataCollection, Section CurrentItem, string MultiBox);
        bool EditBlock(List<Section> DataCollection, Section CurrentItem, string MultiBox);
        bool EditLine(List<Section> DataCollection, Section CurrentItem, string MultiBox);

        Section AddDiagnosis(List<Section> DataCollection, string MultiBox);
        Section AddBlock(List<Section> DataCollection, Section CurrentSection, string MultiBox);
        Section AddLine(List<Section> DataCollection, Section CurrentSection, string MultiBox);
    }
}
