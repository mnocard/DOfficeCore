using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.ViewModels.Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    internal class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel()
        {
            #region Команды
            EditTextCommand = new LambdaCommand(OnEditTextCommandExecuted, CanEditTextCommandExecute);
            CopyTextCommand = new LambdaCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
            SaveDataToFileCommand = new LambdaCommand(OnSaveDataToFileCommandExecuted, CanSaveDataToFileCommandExecute);
            LoadDataCommand = new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
            //GetCellContentCommand = new LambdaCommand(OnGetCellContentCommandExecuted, CanGetCellContentCommandExecute);
            #endregion
        }

        #region Свойства
        #region Заголовок окна
        /// <summary>Заголовок окна</summary>
        private string _Title = "Кабинет врача";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Редактирование текста
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        private bool _EnableTextBox = true;
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        public bool EnableTextBox
        {
            get => _EnableTextBox;
            set => Set(ref _EnableTextBox, value);
        }
        #endregion

        #region Коллекция данных
        /// <summary>Коллекция данных для отправки в дерево</summary>
        private ObservableCollection<Diagnosis> _Diagnoses;
        /// <summary>Коллекция данных для отправки в дерево</summary>
        public ObservableCollection<Diagnosis> Diagnoses
        {
            get => _Diagnoses;
            set => Set(ref _Diagnoses, value);
        }
        #endregion

        //#region Текущая ячейка
        ///// <summary>Текущая ячейка</summary>
        //private DataGridCellInfo _cellInfo;
        ///// <summary>Текущая ячейка</summary>
        //public DataGridCellInfo CellInfo
        //{
        //    get => _cellInfo;
        //    set => _cellInfo = value;
        //}
        //#endregion

        //#region Blocks : ObservableCollection<Block> - Коллекция блоков
        ///// <summary>Коллекция блоков</summary>
        //private ObservableCollection<Block> _Blocks;
        ///// <summary>Коллекция блоков</summary>
        //public ObservableCollection<Block> Blocks
        //{
        //    get => _Blocks;
        //    set => Set(ref _Blocks, value);
        //}
        //#endregion

        #endregion

        #region Команды

        #region Команда редактирования текста
        /// <summary>Команда редактирования текста</summary>
        public ICommand EditTextCommand { get; }
        /// <summary>Команда редактирования текста</summary>
        private void OnEditTextCommandExecuted(object p)
        {
            if (EnableTextBox) EnableTextBox = false;
        }

        private bool CanEditTextCommandExecute(object p)
        {
            return EnableTextBox;
        }
        #endregion

        #region Команда копирования текста
        /// <summary>Команда копирования текста</summary>
        public ICommand CopyTextCommand { get; }
        /// <summary>Команда копирования текста</summary>
        private void OnCopyTextCommandExecuted(object parameter)
        {
            if (parameter as string != null)
            {
                Clipboard.SetText(parameter as string);
                EnableTextBox = true;
            }
        }

        private bool CanCopyTextCommandExecute(object parameter) => true;
        #endregion

        #region Команда сохранения данных в файл
        /// <summary>Команда сохранения данных в файл</summary>
        public ICommand SaveDataToFileCommand { get; }
        /// <summary>Команда сохранения данных в файл</summary>
        private void OnSaveDataToFileCommandExecuted(object p)
        {
            if (p as IEnumerable != null)
            {
                ObservableCollection<Diagnosis> col = new ObservableCollection<Diagnosis>();
                foreach (Diagnosis item in (IEnumerable)p)
                {
                    col.Add(item);
                }
                if(DataProviderService.SaveDataToFile<Diagnosis>(col, "file"))
                {
                    MessageBox.Show("Файл успешно сохранён.");
                }
            }
        }

        private bool CanSaveDataToFileCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда Загрузки данных
        /// <summary>Команда Загрузки данных</summary>
        public ICommand LoadDataCommand { get; }
        /// <summary>Команда Загрузки данных</summary>
        private void OnLoadDataCommandExecuted(object parameter)
        {
            Diagnoses = DataProviderService.LoadDataFromFile("file.json");
        }

        private bool CanLoadDataCommandExecute(object parameter) => true;
        #endregion

        //#region Команда получения данных из выделенной ячейки
        ///// <summary>Команда Загрузки данных</summary>
        //public ICommand GetCellContentCommand { get; }
        ///// <summary>Команда Загрузки данных</summary>
        //private void OnGetCellContentCommandExecuted(object parameter)
        //{
        //    if (parameter as DataGrid != null)
        //    {
        //        Diagnosis dg = (Diagnosis)(parameter as DataGrid).CurrentItem;
        //        Blocks = dg.Blocks;
        //    }
        //}

        //private bool CanGetCellContentCommandExecute(object parameter) => true;

        //#endregion

        #endregion
    }
}
