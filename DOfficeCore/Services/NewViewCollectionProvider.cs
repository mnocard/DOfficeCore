using System;
using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    public class NewViewCollectionProvider : INewViewCollectionProvider
    {
        public Sector SearchDiagnosis(IEnumerable<Sector> SectorsList, string MultiBox) =>
            SectorsList.FirstOrDefault(sector => 
                sector.Name.Equals(MultiBox, StringComparison.CurrentCultureIgnoreCase)
            );

        public List<Block> SearchBlocks(IEnumerable<Sector> SectorsList, string MultiBox) =>
            SectorsList.SelectMany(sector => 
                sector.Blocks.Where(block => 
                    block.Name.Equals(MultiBox, StringComparison.CurrentCultureIgnoreCase)
            )).ToList();

        public List<string> SearchLines(IEnumerable<Sector> SectorsList, string MultiBox) =>
            SectorsList.SelectMany(sector => 
                sector.Blocks.SelectMany(block => 
                    block.Lines.Where(line => 
                        line.Equals(MultiBox, StringComparison.CurrentCultureIgnoreCase)
             ))).ToList();
    }
}
