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

        #region DataCollection : List<Diagnosis> - Коллекция данных из базы данных

        /// <summary>Коллекция данных из базы данных</summary>
        private List<Diagnosis> _DataCollection;

        /// <summary>Коллекция данных из базы данных</summary>
        public List<Diagnosis> DataCollection
        {
            get => _DataCollection;
            set => Set(ref _DataCollection, value);
        }

        #endregion

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

        #region CurrentDiagnosis : string - Текущий выбранный диагноз

        /// <summary>Текущий выбранный диагноз</summary>
        private string _CurrentDiagnosis;

        /// <summary>Текущий выбранный диагноз</summary>
        public string CurrentDiagnosis
        {
            get => _CurrentDiagnosis;
            set => _CurrentDiagnosis = value;
        }

        #endregion

        #region CurrentBlock : string - Текущий выбранный блок

        /// <summary>Текущий выбранный блок</summary>
        private string _CurrentBlock;

        /// <summary>Текущий выбранный блок</summary>
        public string CurrentBlock
        {
            get => _CurrentBlock;
            set => _CurrentBlock = value;
        }

        #endregion

        #region CurrentLine : string - Текущая выбранная строка

        /// <summary>Текущая выбранная строка</summary>
        private string _CurrentLine;

        /// <summary>Текущая выбранная строка</summary>
        public string CurrentLine
        {
            get => _CurrentLine;
            set => _CurrentLine = value;
        }

        #endregion

        #endregion
    }
}
