using System;
using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    ///<inheritdoc cref="INewViewCollectionProvider"/>
    class NewViewCollectionProvider : INewViewCollectionProvider
    {
        ///<inheritdoc/>
        public List<Sector> SearchSectors(IEnumerable<Sector> SectorsList, string MultiBox) =>
            SectorsList.Where(sector => 
                sector.Name.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
            ).Select(sector => sector).ToList();

        ///<inheritdoc/>
        public List<Block> SearchBlocks(IEnumerable<Sector> SectorsList, string MultiBox) =>
            SectorsList.SelectMany(sector => 
                sector.Blocks.Where(block => 
                    block.Name.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
            )).ToList();

        ///<inheritdoc/>
        public List<string> SearchLines(IEnumerable<Sector> SectorsList, string MultiBox) =>
            SectorsList.SelectMany(sector => 
                sector.Blocks.SelectMany(block => 
                    block.Lines.Where(line => 
                        line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
             ))).ToList();

        ///<inheritdoc/>
        public List<Block> GetBlocks(IEnumerable<Sector> SectorsList, Sector Sector) =>
            SectorsList.FirstOrDefault(sector =>
                sector.Name.Equals(Sector.Name)).Blocks;

        ///<inheritdoc/>
        public List<string> GetLines(IEnumerable<Sector> SectorsList, Block Block) =>
            SectorsList.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Lines;
    }
}
