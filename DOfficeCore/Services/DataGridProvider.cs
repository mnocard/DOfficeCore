using DOfficeCore.Models;
using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

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
        /// <param name="DataCollection">Коллекция базы данных, из которой получаем данные</param>
        /// <param name="Provider">Коллекция модели представления, в которой обновляем список диагнозов</param>
        /// <returns></returns>
        public static DataGridProvider UpdateDiagnosisOfProvider(ObservableCollection<Diagnosis> DataCollection, DataGridProvider Provider = null)
        {
            if (DataCollection == null) MessageBox.Show("Ошибка загрузки данных");
            if (Provider == null)
            {
                Provider = new DataGridProvider
                {
                    DiagnosisCode = (ObservableCollection<string>)DataCollection.Select(t => t.Code)
                };
                return Provider;
            }
            else
            {
                var TempCollection = DataCollection.
                    Where(t => Provider.DiagnosisCode.Contains(t.Code)).
                    Select(t => t.Code).
                    Union(Provider.DiagnosisCode);
                Provider.DiagnosisCode = null;
                Provider.DiagnosisCode = new ObservableCollection<string>(TempCollection);
                return Provider;
            }
        }
        #endregion

        #region Метод обновления названий блоков в модели представлении для передачи в датагрид из базы данных

        /// <summary>
        /// Метод обновления названий блоков в модели представлении для передачи в датагрид из базы данных
        /// </summary>
        /// <param name="DataCollection">Коллекция базы данных, из которой получаем данные</param>
        /// <param name="Provider">Коллекция модели представления, в которой обновляем список имён блоков</param>
        /// <param name="DiagnosisCode">Код диагноза, по которому ищем нужную коллекцию блоков</param>
        /// <returns></returns>
        public static DataGridProvider UpdateBlocksOfProvider(ObservableCollection<Diagnosis> DataCollection, DataGridProvider Provider, string DiagnosisCode)
        {
            if (DataCollection == null || Provider == null || DiagnosisCode == null) MessageBox.Show("Ошибка загрузки данных");
            var TempCollection = DataCollection.
                Where(t => t.Code.Equals(DiagnosisCode)).
                Single().Blocks.
                Select(t => t.Name);

            Provider.BlocksNames = null;
            Provider.BlocksNames = new ObservableCollection<string>(TempCollection);
            return Provider;
        }

        #endregion

        #region Метод обновления названий строк в модели представлении для передачи в датагрид из базы данных

        public static DataGridProvider UpdateBlocksOfProvider(ObservableCollection<Diagnosis> DataCollection, DataGridProvider Provider, string DiagnosisCode, string BlocksNames)
        {
            //if (DataCollection == null || Provider == null || DiagnosisCode == null) MessageBox.Show("Ошибка загрузки данных");
            //var TempCollection = DataCollection.
            //    Where(t => t.Code.Equals(DiagnosisCode)).
            //    Select(t => t).


            //Provider.LinesNames = null;
            //Provider.LinesNames = new ObservableCollection<string>(TempCollection);
            //return Provider;
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
