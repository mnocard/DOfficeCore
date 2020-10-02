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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    internal class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(IDataProviderService DataProviderService, IViewCollectionProvider ViewCollectionProvider)
        {
            _DataProviderService = DataProviderService;
            _ViewCollectionProvider = ViewCollectionProvider;
            CurrentData = new ViewCollection();

            #region Команды

            EditTextCommand = new LambdaCommand(OnEditTextCommandExecuted, CanEditTextCommandExecute);
            CopyTextCommand = new LambdaCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
            SaveDataToFileCommand = new LambdaCommand(OnSaveDataToFileCommandExecuted, CanSaveDataToFileCommandExecute);
            LoadDataCommand = new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
            SelectedDataCommand = new LambdaCommand(OnSelectedDataCommandExecuted, CanSelectedDataCommandExecute);
            EditElementCommand = new LambdaCommand(OnEditElementCommandExecuted, CanEditElementCommandExecute);
            StringTransferCommand = new LambdaCommand(OnStringTransferCommandExecuted, CanStringTransferCommandExecute);
            SearchElementCommand = new LambdaCommand(OnSearchElementCommandExecuted, CanSearchElementCommandExecute);

            #endregion
        }

        #region Свойства

        #region Сервис работы с файлами
        private readonly IDataProviderService _DataProviderService;
        #endregion

        #region Сервис работы с данными
        private readonly IViewCollectionProvider _ViewCollectionProvider;
        #endregion

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

        #region Состояние возможности редактирования текстового окна
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        private bool _EnableTextBox = true;
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        public bool EnableTextBox
        {
            get => _EnableTextBox;
            set => Set(ref _EnableTextBox, value);
        }
        #endregion

        #region Активная коллекция данных
        /// <summary>Коллекция данных для отправки в дерево</summary>
        private ViewCollection _CurrentData;
        /// <summary>Коллекция данных для отправки в дерево</summary>
        public ViewCollection CurrentData
        {
            get => _CurrentData;
            set => Set(ref _CurrentData, value);
        }
        #endregion

        #region FocusedDataGrid : DataGrid - Имя датагрида, который сейчас находится в фокусе

        /// <summary>Имя датагрида, который сейчас находится в фокусе</summary>
        private string _FocusedDataGrid;

        /// <summary>Имя датагрида, который сейчас находится в фокусе</summary>
        public string FocusedDataGrid
        {
            get => _FocusedDataGrid;
            set => Set(ref _FocusedDataGrid, value);
        }

        #endregion

        #region MultiBox : string - Содержимое мультибокса

        /// <summary>Содержимое мультибокса</summary>
        private string _MultiBox = "Введите текст";

        /// <summary>Содержимое мультибокса</summary>
        public string MultiBox
        {
            get => _MultiBox;
            set => Set(ref _MultiBox, value);
        }

        #endregion

        #endregion

        #region Команды

        #region Команда редактирования текста в окне дневника
        /// <summary>Команда редактирования текста в окне дневника</summary>
        public ICommand EditTextCommand { get; }
        /// <summary>Команда редактирования текста в окне дневника</summary>
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
            if (parameter is string temp && temp != string.Empty && temp != "")
            {
                Clipboard.SetText(temp);
                EnableTextBox = true;
            }
        }

        private bool CanCopyTextCommandExecute(object parameter)
        {
            if (parameter is string temp && temp != string.Empty && temp != "")
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Команда сохранения данных в файл
        /// <summary>Команда сохранения данных в файл</summary>
        public ICommand SaveDataToFileCommand { get; }
        /// <summary>Команда сохранения данных в файл</summary>
        private void OnSaveDataToFileCommandExecuted(object p)
        {
            if (CurrentData.DataCollection != null) _DataProviderService.SaveDataToFile(CurrentData.DataCollection, "file1");
        }

        private bool CanSaveDataToFileCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда загрузки данных
        /// <summary>Команда Загрузки данных</summary>
        public ICommand LoadDataCommand { get; }
        /// <summary>Команда Загрузки данных</summary>
        private void OnLoadDataCommandExecuted(object parameter)
        {
            CurrentData.DataCollection = _DataProviderService.LoadDataFromFile("file.json");
            CurrentData = _ViewCollectionProvider.DiagnosisFromDataToView(CurrentData);
        }

        private bool CanLoadDataCommandExecute(object parameter) => true;
        #endregion

        #region Команда изменения отображения данных по щелчку
        /// <summary>Команда изменения отображения данных по щелчку</summary>
        public ICommand SelectedDataCommand { get; }
        /// <summary>Команда изменения отображения данных по щелчку</summary>
        private void OnSelectedDataCommandExecuted(object parameter)
        {
            if ((parameter is DataGrid datagrid) && datagrid.CurrentItem != null)
            {
                if (datagrid.Name == "dgCodes")
                {
                    CurrentData.CurrentDiagnosis = (string)datagrid.CurrentItem;
                    CurrentData = _ViewCollectionProvider.BlocksFromDataToView(CurrentData);
                }
                else if (datagrid.Name == "dgBlocksNames")
                {
                    CurrentData.CurrentBlock = (string)datagrid.CurrentItem;
                    CurrentData = _ViewCollectionProvider.LinesFromDataToView(CurrentData);
                }
                else if (datagrid.Name == "dgLinesContent")
                {
                    CurrentData.CurrentLine = (string)datagrid.CurrentItem;
                }
                FocusedDataGrid = datagrid.Name;
            }
        }

        private bool CanSelectedDataCommandExecute(object parameter) => parameter != null;

        #endregion

        #region Команда редактирования выбранного элемента
        /// <summary>Команда редактирования выбранного элемента</summary>
        public ICommand EditElementCommand { get; }
        /// <summary>Команда редактирования выбранного элемента</summary>
        private void OnEditElementCommandExecuted(object parameter)
        {
            if (FocusedDataGrid != null)
            {
                if (FocusedDataGrid == "dgCodes" && !MultiBox.Equals(CurrentData.CurrentDiagnosis))
                {
                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).Code = MultiBox;
                    CurrentData = _ViewCollectionProvider.DiagnosisFromDataToView(CurrentData);
                }
                if (FocusedDataGrid == "dgBlocksNames" && !MultiBox.Equals(CurrentData.CurrentBlock))
                {
                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(CurrentData.CurrentBlock)).Name = MultiBox;
                    CurrentData = _ViewCollectionProvider.BlocksFromDataToView(CurrentData);
                }
                if (FocusedDataGrid == "dgLinesContent" && !MultiBox.Equals(CurrentData.CurrentLine))
                {
                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(CurrentData.CurrentBlock)).
                        Lines.Remove(CurrentData.CurrentLine);

                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(CurrentData.CurrentBlock)).
                        Lines.Add(MultiBox);

                    CurrentData = _ViewCollectionProvider.LinesFromDataToView(CurrentData);
                }

            }
        }

        private bool CanEditElementCommandExecute(object parameter) => true;

        #endregion

        #region Команда отправки элемента из датагрид в текстблок
        /// <summary>Команда отправки элемента из датагрид в текстблок</summary>
        public ICommand StringTransferCommand { get; }
        /// <summary>Команда отправки элемента из датагрид в текстблок</summary>
        private void OnStringTransferCommandExecuted(object parameter)
        {
            if ((parameter is DataGrid datagrid) && datagrid.CurrentItem != null)
            {
                MultiBox = (string)datagrid.CurrentItem;
                FocusedDataGrid = datagrid.Name;
            }
        }

        private bool CanStringTransferCommandExecute(object parameter) => true;

        #endregion

        #region Команда поиска элементов
        /// <summary>Команда поиска элементов</summary>
        public ICommand SearchElementCommand { get; }
        /// <summary>Команда поиска элементов</summary>
        private void OnSearchElementCommandExecuted(object parameter)
        {
            if (MultiBox != string.Empty && MultiBox != "")
            {

            }
        }

        private bool CanSearchElementCommandExecute(object parameter) => true;

        #endregion


        #endregion
    }
}
