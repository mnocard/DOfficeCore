using System.Collections.Generic;
using System.Linq;

namespace DOfficeCore.Models
{
    /// <summary>Сектор, корневой элемент базы данных</summary>
    public class Sector
    {
        /// <summary>Название сектора</summary>
        public string Name { get; set; }

        /// <summary>Список разделов сектора</summary>
        public List<Block> Blocks { get; set; } = new List<Block>();

        /// <summary>Клонирование секторов</summary>
        /// <param name="other">Клонируемый сектор</param>
        /// <returns>Клонированный сектор</returns>
        public static Sector CloneSector(Sector other) => other is null
            ? null
            : new Sector
            {
                Name = other.Name,
                Blocks = new List<Block>(other.Blocks),
            };

        /// <summary>Сравнение секторов</summary>
        /// <param name="other">Сектор, с которым происходит сравнение</param>
        /// <returns>True - если секторы идентичны</returns>
        public bool Equals(Sector other)
        {
            if (this is null || other is null)
                return this is null && other is null;

            if (Name is null || other.Name is null)
                return Name is null && other.Name is null;

            if (Name.Equals(other.Name))
            {
                if (Blocks is null || other.Blocks is null)
                    return Blocks is null && other.Blocks is null;

                if (Blocks.Count == other.Blocks.Count)
                {
                    var firstNotSecond = Blocks.Except(other.Blocks).ToList();
                    var secondNotFirst = other.Blocks.Except(Blocks).ToList();
                    return !firstNotSecond.Any() && !secondNotFirst.Any();
                }
            }

            return false;
        }
        /// <summary>Сравнение сектора с object</summary>
        /// <param name="other">Объект, с которым происходит сравнение</param>
        /// <returns>True - если секторы идентичны</returns>
        public override bool Equals(object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            return Equals(other as Sector);
        }
    }
}
