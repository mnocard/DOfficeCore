using System.Collections.Generic;
using System.Linq;

namespace DOfficeCore.Models
{
    public class Sector
    {
        public string Name { get; set; }
        public List<Block> Blocks { get; set; } = new List<Block>();

        public static Sector CloneSector(Sector other) => other is null
            ? null
            : new Sector
            {
                Name = other.Name,
                Blocks = new List<Block>(other.Blocks),
            };

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

        public override bool Equals(object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            return Equals(other as Sector);
        }
    }
}
