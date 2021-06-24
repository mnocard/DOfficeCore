using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    public interface INewCollectionHandler
    {
        bool AddSector(List<Sector> SectorsCollection, string MultiBox);
        bool AddBlock(List<Sector> SectorsCollection, Sector Sector, string MultiBox);
        bool AddLine(List<Sector> SectorsCollection, Block Block, string MultiBox);

        bool RemoveSector(List<Sector> SectorsCollection, Sector Sector);
        bool RemoveBlock(List<Sector> SectorsCollection, Sector Sector, Block Block);
        bool RemoveLine(List<Sector> SectorsCollection, Block Block, string Line);

        bool EditSector(List<Sector> SectorsCollection, Sector Sector, string MultiBox);
        bool EditBlock(List<Sector> SectorsCollection, Block Block, string MultiBox);
        bool EditLine(List<Sector> SectorsCollection, Block Block, string Line, string MultiBox);
    }
}
