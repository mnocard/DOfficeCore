using System;

namespace DOfficeCore.Models
{
    class Section : IEquatable<Section>
    {
        // Diagnosis : string - Диагноз, в котором находится раздел
        public string Diagnosis { get; set; }

        // Block : string - Раздел, в котором находится строка
        public string Block { get; set; }

        // Line : string - Собственно сама строка
        public string Line { get; set; }

        public bool Equals(Section other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(Object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            //if (other is Section otherSection)
            //    return Equals(otherSection);
            //else
            //    return false;

            return Equals(other as Section);
        }
        public override int GetHashCode() => HashCode.Combine(Diagnosis, Block, Line);
    }
}
