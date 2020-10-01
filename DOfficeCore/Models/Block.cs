using DOfficeCore.ViewModels.Core;
using System.Collections.ObjectModel;

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

        #region Lines : ObservableCollection<string> - Список строк в блоке
        /// <summary>Список строк в блоке</summary>
        private ObservableCollection<string> _Lines;
        /// <summary>Список строк в блоке</summary>
        public ObservableCollection<string> Lines
        {
            get => _Lines;
            set => Set(ref _Lines, value);
        }
        #endregion
    }
}
