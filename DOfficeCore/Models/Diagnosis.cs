using DOfficeCore.ViewModels.Core;
using System.Collections.Generic;

namespace DOfficeCore.Models
{
    class Diagnosis : ViewModelCore
    {
        #region Code : string - Код диагноза
        /// <summary>Код диагноза</summary>
        private string _Code;
        /// <summary>Код диагноза</summary>
        public string Code
        {
            get => _Code;
            set => Set(ref _Code, value);
        }
        #endregion

        #region Blocks : HashSet<Blocks> - Уникальные блоки диагноза
        /// <summary>Уникальные блоки диагноза</summary>
        private HashSet<Block> _Blocks;
        /// <summary>Уникальные блоки диагноза</summary>
        public HashSet<Block> Blocks
        {
            get => _Blocks;
            set => Set(ref _Blocks, value);
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            Diagnosis diagnosis = (Diagnosis)obj;
            return Code == diagnosis.Code;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
