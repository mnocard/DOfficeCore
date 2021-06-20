using System.Collections.Generic;
using System.Linq;

namespace DOfficeCore.Models
{
    public class Block
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        List<string> Lines { get; set; } = new List<string>();

        public static Block CloneBlock(Block other) => other is null
            ? null
            : new Block
            {
                Name = other.Name,
                Sector = other.Sector,
                Lines = new List<string>(other.Lines),
            };

        public bool Equals(Block other)
        {
            if (this is null || other is null)
                return this is null && other is null;

            if (Name is null || other.Name is null)
                return Name is null && other.Name is null;

            if (Name.Equals(other.Name))
            {
                if (Sector is null || other.Sector is null)
                    return Sector is null && other.Sector is null;

                if (Sector.Equals(other.Sector))
                {
                    if (Lines is null || other.Lines is null)
                        return Lines is null && other.Lines is null;

                    if (Lines.Count == other.Lines.Count)
                    {
                        var firstNotSecond = Lines.Except(other.Lines).ToList();
                        var secondNotFirst = other.Lines.Except(Lines).ToList();
                        return !firstNotSecond.Any() && !secondNotFirst.Any();
                    }
                }
            }

            return false;
        }

        public override bool Equals(object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            return Equals(other as Block);
        }
    }
}
