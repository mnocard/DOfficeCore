using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    public interface INewViewCollectionProvider
    {
        List<Sector> SearchSectors(IEnumerable<Sector> SectorsList, string MultiBox);
        List<Block> SearchBlocks(IEnumerable<Sector> SectorsList, string MultiBox);
        List<string> SearchLines(IEnumerable<Sector> SectorsList, string MultiBox);
    }
}
