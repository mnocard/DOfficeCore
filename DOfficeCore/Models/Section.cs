using System;
using DOfficeCore.ViewModels.Core;

namespace DOfficeCore.Models
{
    class Section : ViewModelCore, IEquatable<Section>
    {
        #region Diagnosis : string - Диагноз, в котором находится раздел

        /// <summary>Диагноз, в котором находится раздел</summary>
        private string _Diagnosis;

        /// <summary>Диагноз, в котором находится раздел</summary>
        public string Diagnosis
        {
            get => _Diagnosis;
            set => Set(ref _Diagnosis, value);
        }

        #endregion

        #region Block : string - Раздел, в котором находится строка

        /// <summary>Раздел, в котором находится строка</summary>
        private string _Block;

        /// <summary>Раздел, в котором находится строка</summary>
        public string Block
        {
            get => _Block;
            set => Set(ref _Block, value);
        }

        #endregion

        #region Line : string - Собственно сама строка

        /// <summary>Собственно сама строка</summary>
        private string _Line;

        /// <summary>Собственно сама строка</summary>
        public string Line
        {
            get => _Line;
            set => Set(ref _Line, value);
        }

        #endregion

        public bool Equals(Section other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            if (this.Diagnosis.Equals(other.Diagnosis) &&
                this.Block.Equals(other.Block) &&
                this.Line.Equals(other.Line))
                return true;
            else
                return false;
        }

        public override bool Equals(Object other)
        {
            if (this == null && other == null) return true;
            if (this == null || other == null) return false;

            if (other is Section otherSection)
                return Equals(otherSection);
            else
                return false;
        }
        public override int GetHashCode() => HashCode.Combine(Diagnosis, Block, Line);
    }
}
