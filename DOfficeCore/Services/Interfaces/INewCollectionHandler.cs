using System.Collections.ObjectModel;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    public interface INewCollectionHandler
    {
        bool AddSector(ObservableCollection<Sector> SectorCollection, string MultiBox);
        bool AddBlock(Sector Sector, string MultiBox);
        bool AddLine(Block Block, string MultiBox);

        bool RemoveSector(ObservableCollection<Sector> SectorCollection, Sector Sector);
        bool RemoveBlock(Sector Sector, Block Block);
        bool RemoveLine(Block Block, string Line);

        bool EditSector(Sector Sector, string MultiBox);
        bool EditBlock(Block Block, string MultiBox);
        bool EditLine(Block Block, string Line, string MultiBox);
    }
}
