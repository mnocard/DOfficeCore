using DOfficeCore.Models;
using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DOfficeCore.Services
{
    class DataGridProvider : ViewModelCore
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

        #region Методы

        #region Метод обновления коллекции во модели представлении для передачи в датагрид из базы данных

        /// <summary>
        /// Метод обновления кодов диагнозов в модели представлении для передачи в датагрид из базы данных
        /// </summary>
        /// <param name="DataCollection">Коллекция базы данных</param>
        /// <param name="DataGridProvider">Коллекция модели представления</param>
        /// <returns></returns>
        public static DataGridProvider UpdateDiagnosisOfProvider(ObservableCollection<Diagnosis> DataCollection, DataGridProvider Provider = null)
        {
            //if (Provider == null)
            //{
            //    Provider = new DataGridProvider();
            //    foreach (Diagnosis item in DataCollection)
            //    {
            //        Provider.DiagnosisCode.Add(item.Code);
            //    }
            //}
            //else
            //{
            //    foreach (Diagnosis item in DataCollection)
            //    {
            //        if (!Provider.DiagnosisCode.Contains(item.Code)) Provider.DiagnosisCode.Add(item.Code);
            //    }
            //    foreach (string item in Provider.DiagnosisCode)
            //    {
                    
            //    }
            //}
        }

        #endregion

        public static ObservableCollection<Diagnosis> UpdateDiagnosisOfModel(IList<Diagnosis> DataCollection, DataGridProvider VMCollection)
        {
            //if (VMCollection.Count == DataCollection.Count)
            
        }



        //public static ObservableCollection<Diagnosis> UpdateSelfCollection(IList<Diagnosis> VMCollection, ObservableCollection<DataGridProvider> DataCollection, int IndexOfDiagnosis, int IndexOfBlock)
        //{

        //    foreach (Diagnosis item in VMCollection)
        //    {
        //        DataCollection.Add(new DataGridProvider(item.Code, string.Empty, string.Empty));
        //    }

        //    if(IndexOfDiagnosis >= 0 && IndexOfDiagnosis < VMCollection.Count)
        //    {
        //        IList<Block> TempBlockCollection = VMCollection[IndexOfDiagnosis].Blocks;

        //        for (int i = 0; i < TempBlockCollection.Count; i++)
        //        {
        //            DataCollection[i].dgBlock = TempBlockCollection[i].Name;
        //        }

        //        if (IndexOfBlock >= 0 && IndexOfBlock < VMCollection[IndexOfDiagnosis].Blocks.Count)
        //        {
        //            IList<string> TempStringCollection = VMCollection[IndexOfDiagnosis].Blocks[IndexOfBlock].Lines;

        //            for (int i = 0; i < TempStringCollection.Count; i++)
        //            {
        //                DataCollection[i].dgLines = TempStringCollection[i];
        //            }
        //        }
        //    }

        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
