using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    ///<inheritdoc cref="INewCollectionHandler"/>
    class NewCollectionHandler : INewCollectionHandler
    {
        #region Добавление элемента
        ///<inheritdoc/>
        public bool AddSector(List<Sector> SectorsCollection, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorsCollection.Any(section =>
                    section.Name.Equals(MultiBox)))
                return false;

            SectorsCollection.Add(new Sector { Name = MultiBox });
            return true;
        }

        ///<inheritdoc/>
        public bool AddBlock(List<Sector> SectorsCollection, Sector Sector, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorsCollection.Any(sector =>
                    sector.Name.Equals(Sector.Name) &&
                    sector.Blocks.Any(block =>
                        block.Name is not null &&
                        block.Name.Equals(MultiBox))))
                return false;

            SectorsCollection.FirstOrDefault(sector =>
                sector.Name.Equals(Sector.Name)).Blocks.Add(new Block
                {
                    Name = MultiBox,
                    Sector = Sector.Name,
                });

            return true;
        }

        ///<inheritdoc/>
        public bool AddLine(List<Sector> SectorsCollection, Block Block, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorsCollection.Any(sector =>
                    sector.Name.Equals(Block.Sector) &&
                    sector.Blocks.Any(block =>
                        block.Lines.Any(line =>
                            line.Equals(MultiBox)))))
                return false;

            SectorsCollection.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Lines.Add(MultiBox);

            return true;
        }

        #endregion

        #region Удаление элемента

        ///<inheritdoc/>
        public bool RemoveSector(List<Sector> SectorsCollection, Sector Sector)
        {
            if (Sector is null)
                return false;
            return SectorsCollection.Remove(Sector);
        }

        ///<inheritdoc/>
        public bool RemoveBlock(List<Sector> SectorsCollection, Sector Sector, Block Block)
        {
            if (SectorsCollection is null ||
                Sector is null ||
                Block is null)
                return false;

            return SectorsCollection.FirstOrDefault(sector =>
                sector.Name.Equals(Sector.Name)).Blocks.Remove(Block);
        }

        ///<inheritdoc/>
        public bool RemoveLine(List<Sector> SectorsCollection, Block Block, string Line)
        {
            if (SectorsCollection is null ||
                Block is null ||
                string.IsNullOrWhiteSpace(Line))
                return false;

            return SectorsCollection.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Lines.Remove(Line);
        }

        #endregion

        #region Изменение элемента

        ///<inheritdoc/>
        public bool EditSector(List<Sector> SectorsCollection, Sector Sector, string MultiBox)
        {
            if (SectorsCollection is null ||
                string.IsNullOrWhiteSpace(MultiBox) ||
                Sector is null ||
                Sector.Name.Equals(MultiBox))
                return false;

            SectorsCollection.FirstOrDefault(sector =>
                sector.Name.Equals(Sector.Name)).Name = MultiBox;

            SectorsCollection.FirstOrDefault(sector =>
                 sector.Name.Equals(Sector.Name)).Blocks.ForEach(block =>
                    block.Sector = MultiBox);

            return true;
        }

        ///<inheritdoc/>
        public bool EditBlock(List<Sector> SectorsCollection, Block Block, string MultiBox)
        {
            if (SectorsCollection is null ||
                string.IsNullOrWhiteSpace(MultiBox) ||
                Block is null ||
                Block.Name.Equals(MultiBox))
                return false;

            SectorsCollection.FirstOrDefault(sector =>
                sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                    block.Name.Equals(Block.Name)).Name = MultiBox;
            return true;
        }

        ///<inheritdoc/>
        public bool EditLine(List<Sector> SectorsCollection, Block Block, string Line, string MultiBox)
        {
            if (SectorsCollection is null ||
                string.IsNullOrWhiteSpace(MultiBox) ||
                string.IsNullOrWhiteSpace(Line) ||
                Block is null ||
                !Block.Lines.Any(l => l.Equals(Line)))
                return false;

            if (SectorsCollection.FirstOrDefault(sector =>
                 sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                     block.Name.Equals(Block.Name)).Lines.Remove(Line))
            {
                SectorsCollection.FirstOrDefault(sector =>
                    sector.Name.Equals(Block.Sector)).Blocks.FirstOrDefault(block =>
                        block.Name.Equals(Block.Name)).Lines.Add(MultiBox);
                return true;
            }
            return false;
        }

        #endregion
    }
}
