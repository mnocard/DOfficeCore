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

        #region Blocks : List<Blocks> - Уникальные блоки диагноза
        /// <summary>Уникальные блоки диагноза</summary>
        private List<Block> _Blocks;
        /// <summary>Уникальные блоки диагноза</summary>
        public List<Block> Blocks
        {
            get => _Blocks;
            set => Set(ref _Blocks, value);
        }
        #endregion
    }
}
