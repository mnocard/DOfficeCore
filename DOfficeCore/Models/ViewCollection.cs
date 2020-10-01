using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DOfficeCore.Models
{
    class ViewCollection : ViewModelCore
    {
        #region Свойства

        #region DiagnosisCode : ObservableCollection<string> - Коллекция кодов диагнозов

        /// <summary>Коллекция кодов диагнозов</summary>
        private ObservableCollection<string> _DiagnosisCode;

        /// <summary>Коллекция кодов диагнозов</summary>
        public ObservableCollection<string> DiagnosisCode
        {
            get => _DiagnosisCode;
            set => Set(ref _DiagnosisCode, value);
        }

        #endregion

        #region BlocksNames : ObservableCollection<string> - Коллекция названий блоков

        /// <summary>Коллекция названий блоков</summary>
        private ObservableCollection<string> _BlocksNames;

        /// <summary>Коллекция названий блоков</summary>
        public ObservableCollection<string> BlocksNames
        {
            get => _BlocksNames;
            set => Set(ref _BlocksNames, value);
        }

        #endregion

        #region LinesNames : ObservableCollection<string> - Коллекция содержимого строк

        /// <summary>Коллекция содержимого строк</summary>
        private ObservableCollection<string> _LinesNames;

        /// <summary>Коллекция содержимого строк</summary>
        public ObservableCollection<string> LinesNames
        {
            get => _LinesNames;
            set => Set(ref _LinesNames, value);
        }

        #endregion

        #endregion


    }
}
