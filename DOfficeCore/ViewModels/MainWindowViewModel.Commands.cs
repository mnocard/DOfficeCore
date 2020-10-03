﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    partial class MainWindowViewModel
    {
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

        private bool CanCopyTextCommandExecute(object parameter) => parameter is string temp && temp != string.Empty && temp != "";
        #endregion

        #region Команда сохранения данных в файл
        /// <summary>Команда сохранения данных в файл</summary>
        public ICommand SaveDataToFileCommand { get; }
        /// <summary>Команда сохранения данных в файл</summary>
        private void OnSaveDataToFileCommandExecuted(object p)
        {
            if (_ViewCollection.DataCollection != null) 
                _DataProviderService.SaveDataToFile(_ViewCollection.DataCollection, "file1");
        }

        private bool CanSaveDataToFileCommandExecute(object p) => true;
        #endregion

        #region Команда загрузки данных
        /// <summary>Команда Загрузки данных</summary>
        public ICommand LoadDataCommand { get; }
        /// <summary>Команда Загрузки данных</summary>
        private void OnLoadDataCommandExecuted(object parameter)
        {
            _ViewCollection.DataCollection = _DataProviderService.LoadDataFromFile("file.json");
            _ViewCollectionProvider.DiagnosisFromDataToView();
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
                FocusedDataGrid = datagrid.Name;
                _ViewCollectionProvider.SelectedData(FocusedDataGrid, (string)datagrid.CurrentItem);
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
            if (FocusedDataGrid != null) _ViewCollectionProvider.EditElement(FocusedDataGrid, MultiBox);
        }

        private bool CanEditElementCommandExecute(object parameter) => true;

        #endregion

        #region Команда отправки элемента из датагрид в мультибокс
        /// <summary>Команда отправки элемента из датагрид в мультибокс</summary>
        public ICommand StringTransferCommand { get; }
        /// <summary>Команда отправки элемента из датагрид в мультибокс</summary>
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
            if (MultiBox != null && MultiBox != string.Empty && MultiBox.Length > 3)
            {
                _ViewCollectionProvider.SearchElement(MultiBox);
                FocusedDataGrid = null;
            }
        }

        private bool CanSearchElementCommandExecute(object parameter) => true;

        #endregion

        #region Команда удаления элементов из списка
        /// <summary>Команда удаления элементов из списка</summary>
        public ICommand RemoveElementCommand { get; }
        /// <summary>Команда удаления элементов из списка</summary>
        private void OnRemoveElementCommandExecuted(object parameter)
        {
            _ViewCollectionProvider.RemoveElement(FocusedDataGrid, MultiBox);
        }

        private bool CanRemoveElementCommandExecute(object parameter) => FocusedDataGrid != null && MultiBox != null;

        #endregion

        #region Команда добавления элемента
        /// <summary>Команда добавления элемента</summary>
        public ICommand AddElementCommand { get; }
        /// <summary>Команда добавления элемента</summary>
        private void OnAddElementCommandExecuted(object parameter)
        {
            
        }

        private bool CanAddElementCommandExecute(object parameter) => true;


        #endregion
        #endregion
    }
}
