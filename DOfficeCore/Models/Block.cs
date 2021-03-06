﻿using DOfficeCore.ViewModels.Core;
using System.Collections.Generic;

namespace DOfficeCore.Models
{
    /// <summary>Элемент содержащий строки дневника</summary>
    class Block : ViewModelCore
    {
        #region Name : string - Название блока
        /// <summary>Название блока</summary>
        private string _Name;
        /// <summary>Название блока</summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        #endregion

        #region Lines : List<string> - Список строк в блоке
        /// <summary>Список строк в блоке</summary>
        private List<string> _Lines;
        /// <summary>Список строк в блоке</summary>
        public List<string> Lines
        {
            get => _Lines;
            set => Set(ref _Lines, value);
        }
        #endregion
    }
}
