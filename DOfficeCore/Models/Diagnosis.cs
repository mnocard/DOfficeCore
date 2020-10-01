using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region UniqueBlocks : ObservableCollection<Blocks> - Уникальные блоки диагноза
        /// <summary>Уникальные блоки диагноза</summary>
        private ObservableCollection<Block> _Blocks;
        /// <summary>Уникальные блоки диагноза</summary>
        public ObservableCollection<Block> Blocks
        {
            get => _Blocks;
            set => Set(ref _Blocks, value);
        }
        #endregion
    }
}
