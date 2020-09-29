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
        public DataGridProvider(string dgDiagnosis, string dgBlock, string dgLines)
        {
            this.dgDiagnosis = dgDiagnosis;
            this.dgBlock = dgBlock;
            this.dgLines = dgLines;
        }

        #region Свойства

        #region dgDiagnosis : string - Название диагноза
        /// <summary>Название диагноза</summary>
        private string _dgDiagnosis;
        /// <summary>Название диагноза</summary>
        public string dgDiagnosis
        {
            get => _dgDiagnosis;
            set => Set(ref _dgDiagnosis, value);
        }
        #endregion

        #region dgBlock : string - Название блока
        /// <summary>Название блока</summary>
        private string _dgBlock;
        /// <summary>Название блока</summary>
        public string dgBlock
        {
            get => _dgBlock;
            set => Set(ref _dgBlock, value);
        }
        #endregion

        #region dgLines : string - Название строки
        /// <summary>Название строки</summary>
        private string _dgLines;
        /// <summary>Название строки</summary>
        public string dgLines
        {
            get => _dgLines;
            set => Set(ref _dgLines, value);
        }
        #endregion

        #endregion

        #region Методы

        #region Метод обновления коллекции во модели представлении для передачи в датагрид из базы данных

        /// <summary>
        /// Метод обновления коллекции во модели представлении для передачи в датагрид из базы данных
        /// </summary>
        /// <param name="DataCollection">Коллекция базы данных</param>
        /// <param name="VMCollection">Коллекция модели представления</param>
        /// <returns></returns>
        public static ObservableCollection<DataGridProvider> UpdateDiagnosisOfProvider(IList<Diagnosis> DataCollection, ObservableCollection<DataGridProvider> VMCollection = null)
        {
            if (VMCollection == null)
            {
                for (int i = 0; i < DataCollection.Count; i++)
                {
                    VMCollection.Add(new DataGridProvider(DataCollection[i].Code, string.Empty, string.Empty));
                }
                return VMCollection;
            }
            else
            {
                ObservableCollection<DataGridProvider> NewCollection = new ObservableCollection<DataGridProvider>();
                for (int i = 0; i < DataCollection.Count; i++)
                {
                    NewCollection.Add(new DataGridProvider(DataCollection[i].Code, VMCollection[i].dgBlock, VMCollection[i].dgLines));
                }
                return NewCollection;
            }
        }

        #endregion

        public static IList<Diagnosis> UpdateDiagnosisOfModel(IList<Diagnosis> DataCollection, ObservableCollection<DataGridProvider> VMCollection)
        {
            if (VMCollection.Count == DataCollection.Count)
            for (int i = 0; i < DataCollection.Count; i++)
            {
                if(!Equals(DataCollection[i].Code, VMCollection[i].dgDiagnosis))
                {
                    DataCollection[i].Code = VMCollection[i].dgDiagnosis;
                }
            }
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

        public static ObservableCollection<Diagnosis> UpdateDiagnosisCollection(ObservableCollection<Diagnosis> VMCollection, ObservableCollection<DataGridProvider> DataCollection, int IndexOfDiagnosis, int IndexOfBlock)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
