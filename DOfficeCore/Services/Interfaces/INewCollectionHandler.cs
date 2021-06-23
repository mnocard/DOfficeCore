using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    public interface INewCollectionHandler
    {
        bool AddSector(List<Sector> SectorList, string MultiBox);
        bool AddBlock(List<Sector> SectorList, Sector Sector, string MultiBox);
        bool AddLine(List<Sector> SectorList, Block Block, string MultiBox);

        bool RemoveSector(List<Sector> SectorList, Sector Sector);
        bool RemoveBlock(List<Sector> SectorList, Sector Sector, Block Block);
        bool RemoveLine(List<Sector> SectorList, Block Block, string Line);

        bool EditSector(List<Sector> SectorList, Sector Sector, string MultiBox);
        bool EditBlock(List<Sector> SectorList, Block Block, string MultiBox);
        bool EditLine(List<Sector> SectorList, Block Block, string Line, string MultiBox);
    }
}
