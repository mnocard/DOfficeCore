using System.Collections.ObjectModel;
using System.Linq;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    public class NewCollectionHandler : INewCollectionHandler
    {
        #region Добавление элемента
        public bool AddSector(ObservableCollection<Sector> SectorCollection, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                SectorCollection.Any(section => section.Name.Equals(MultiBox)))
                return false;

            SectorCollection.Add(new Sector { Name = MultiBox });
            return true;
        }

        public bool AddBlock(Sector Sector, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                Sector.Blocks.Any(block =>
                    block.Name is not null &&
                    block.Name.Equals(MultiBox)))
                return false;

            Sector.Blocks.Add(new Block
            {
                Name = MultiBox,
                Sector = Sector.Name,
            });
            return true;
        }

        public bool AddLine(Block Block, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                Block.Lines.Any(line => line.Equals(MultiBox)))
                return false;

            Block.Lines.Add(MultiBox);
            return true;
        }

        #endregion

        #region Удаление элемента

        public bool RemoveSector(ObservableCollection<Sector> SectorCollection, Sector Sector)
        {
            if (Sector is null)
                return false;
            SectorCollection.Remove(Sector);
            return true;
        }

        public bool RemoveBlock(Sector Sector, Block Block)
        {
            if (Block is null)
                return false;
            Sector.Blocks.Remove(Block);
            return true;
        }

        public bool RemoveLine(Block Block, string Line)
        {
            if (string.IsNullOrWhiteSpace(Line))
                return false;
            Block.Lines.Remove(Line);
            return true;
        }

        #endregion

        #region Изменение элемента

        public bool EditSector(Sector Sector, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                Sector is null ||
                !Sector.Name.Equals(MultiBox))
                return false;

            Sector.Name = MultiBox;
            return true;
        }

        public bool EditBlock(Block Block, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                Block is null ||
                !Block.Name.Equals(MultiBox))
                return false;

            Block.Name = MultiBox;
            return true;
        }

        public bool EditLine(Block Block, string Line, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                string.IsNullOrWhiteSpace(Line) ||
                Block is null ||
                !Block.Lines.Any(l => l.Equals(Line)))
                return false;

            Block.Lines.Remove(Line);
            Block.Lines.Add(MultiBox);
            return true;
        }

        #endregion
    }
}
