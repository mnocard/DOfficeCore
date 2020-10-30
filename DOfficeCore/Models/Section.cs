using System;
using DOfficeCore.ViewModels.Core;

namespace DOfficeCore.Models
{
    class Section : ViewModelCore
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

        //public override bool Equals(object obj) => Equals(obj as Section);
        //public bool Equals(Section other)
        //{
        //    return other != null &&
        //        Diagnosis != null &&
        //        Block != null &&
        //        Line != null &&
        //        Diagnosis.Equals(other.Diagnosis) &&
        //        Block.Equals(other.Block) &&
        //        Line.Equals(other.Line);
        //}

        //public override int GetHashCode() => HashCode.Combine(Diagnosis, Block, Line);
    }
}
