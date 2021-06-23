using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    public class NewCollectionHandler : INewCollectionHandler
    {
        #region Добавление элемента
        public bool AddSector(List<Sector> SectorList, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorList.Any(section => 
                    section.Name.Equals(MultiBox)))
                return false;

            SectorList.Add(new Sector { Name = MultiBox });
            return true;
        }

        public bool AddBlock(List<Sector> SectorList, Sector Sector, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorList.Any(sector => 
                    sector.Name.Equals(Sector.Name) && 
                    sector.Blocks.Any(block =>
                        block.Name is not null &&
                        block.Name.Equals(MultiBox))))
                return false;

            SectorList.FirstOrDefault(sector => 
                sector.Name.Equals(Sector.Name)).Blocks.Add(new Block
                {
                    Name = MultiBox,
                    Sector = Sector.Name,
                });

            return true;
        }

        public bool AddLine(List<Sector> SectorList, Block Block, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorList.Any(sector => 
                    sector.Name.Equals(Block.Sector) &&
                    sector.Blocks.Any(block => 
                        block.Lines.Any(line => 
                            line.Equals(MultiBox)))))
                return false;

            SectorList.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Lines.Add(MultiBox);

            return true;
        }

        #endregion

        #region Удаление элемента

        public bool RemoveSector(List<Sector> SectorList, Sector Sector)
        {
            if (Sector is null)
                return false;
            return SectorList.Remove(Sector);
        }

        public bool RemoveBlock(List<Sector> SectorList, Sector Sector, Block Block)
        {
            if (SectorList is null ||
                Sector is null ||
                Block is null)
                return false;

            return SectorList.FirstOrDefault(sector => 
                sector.Name.Equals(Sector.Name)).Blocks.Remove(Block);
        }

        public bool RemoveLine(List<Sector> SectorList, Block Block, string Line)
        {
            if (SectorList is null ||
                Block is null ||
                string.IsNullOrWhiteSpace(Line))
                return false;

            return SectorList.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Lines.Remove(Line);
        }

        #endregion

        #region Изменение элемента

        public bool EditSector(List<Sector> SectorList, Sector Sector, string MultiBox)
        {
            if (SectorList is null || 
                string.IsNullOrWhiteSpace(MultiBox) ||
                Sector is null ||
                !Sector.Name.Equals(MultiBox))
                return false;

            SectorList.FirstOrDefault(sector =>
                sector.Name.Equals(Sector.Name)).Name = MultiBox;

            return true;
        }

        public bool EditBlock(List<Sector> SectorList, Block Block, string MultiBox)
        {
            if (SectorList is null || 
                string.IsNullOrWhiteSpace(MultiBox) ||
                Block is null ||
                !Block.Name.Equals(MultiBox))
                return false;

            SectorList.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Name = MultiBox;
            return true;
        }

        public bool EditLine(List<Sector> SectorList, Block Block, string Line, string MultiBox)
        {
            if (SectorList is null || 
                string.IsNullOrWhiteSpace(MultiBox) ||
                string.IsNullOrWhiteSpace(Line) ||
                Block is null ||
                !Block.Lines.Any(l => l.Equals(Line)))
                return false;

            SectorList.FirstOrDefault(sector =>
                 sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                     block.Name.Equals(Block.Name)).Lines.Remove(Line);

            SectorList.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Lines.Add(MultiBox);

            return true;
        }

        #endregion
    }
}
