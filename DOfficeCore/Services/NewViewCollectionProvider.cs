using System;
using System.Collections.Generic;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    public class NewViewCollectionProvider : INewViewCollectionProvider
    {
        public Sector SearchDiagnosis(IEnumerable<Sector> SectorsList, string MultiBox)
        {
            throw new NotImplementedException();
        }

        public List<Block> SearchBlocks(IEnumerable<Sector> SectorsList, string MultiBox)
        {
            throw new NotImplementedException();
        }

        public List<string> SearchLines(IEnumerable<Sector> SectorsList, string MultiBox)
        {
            throw new NotImplementedException();
        }
    }
}
