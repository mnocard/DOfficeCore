using System;
using DOfficeCore.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DOfficeCore.Infrastructure.Commands;

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
            if (_ViewCollection.DataCollection != null) _DataProviderService.SaveDataToFile(_ViewCollection.DataCollection, "file1");
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
                if (datagrid.Name == "dgCodes")
                {
                    _ViewCollection.CurrentDiagnosis = (string)datagrid.CurrentItem;
                    _ViewCollectionProvider.BlocksFromDataToView();
                }
                else if (datagrid.Name == "dgBlocksNames")
                {
                    _ViewCollection.CurrentBlock = (string)datagrid.CurrentItem;
                    _ViewCollectionProvider.LinesFromDataToView();
                }
                else if (datagrid.Name == "dgLinesContent")
                {
                    _ViewCollection.CurrentLine = (string)datagrid.CurrentItem;
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
                if (FocusedDataGrid == "dgCodes" && !MultiBox.Equals(_ViewCollection.CurrentDiagnosis))
                {
                    _ViewCollection.DataCollection.Find(t => t.Code.Equals(_ViewCollection.CurrentDiagnosis)).Code = MultiBox;
                    _ViewCollectionProvider.DiagnosisFromDataToView();
                }
                if (FocusedDataGrid == "dgBlocksNames" && !MultiBox.Equals(_ViewCollection.CurrentBlock))
                {
                    _ViewCollection.DataCollection.Find(t => t.Code.Equals(_ViewCollection.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(_ViewCollection.CurrentBlock)).Name = MultiBox;
                    _ViewCollectionProvider.BlocksFromDataToView();
                }
                if (FocusedDataGrid == "dgLinesContent" && !MultiBox.Equals(_ViewCollection.CurrentLine))
                {
                    _ViewCollection.DataCollection.Find(t => t.Code.Equals(_ViewCollection.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(_ViewCollection.CurrentBlock)).
                        Lines.Remove(_ViewCollection.CurrentLine);

                    _ViewCollection.DataCollection.Find(t => t.Code.Equals(_ViewCollection.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(_ViewCollection.CurrentBlock)).
                        Lines.Add(MultiBox);

                    _ViewCollectionProvider.LinesFromDataToView();
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
            if (MultiBox != string.Empty)
            {
                _ViewCollection.LinesNames = null;
                _ViewCollection.LinesNames = new ObservableCollection<string>();
                foreach (var line in from Diagnosis diagnosis in _ViewCollection.DataCollection
                                     from Block block in diagnosis.Blocks
                                     from string line in block.Lines
                                     where line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
                                     select line)
                {
                    _ViewCollection.LinesNames.Add(line);
                }
            }
            else if (MultiBox != null)
            {
                _ViewCollectionProvider.DiagnosisFromDataToView();
                _ViewCollectionProvider.BlocksFromDataToView();
                _ViewCollectionProvider.LinesFromDataToView();
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
            if (FocusedDataGrid != null && MultiBox != null)
            {
                if (FocusedDataGrid == "dgCodes" && MultiBox.Equals(_ViewCollection.CurrentDiagnosis))
                {
                    for (int i = 0; i < _ViewCollection.DataCollection.Count; i++)
                    {
                        if (_ViewCollection.DataCollection[i].Code.Equals(MultiBox))
                        {
                            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с названием: \"{MultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                _ViewCollection.DataCollection.RemoveAt(i);
                                _ViewCollectionProvider.DiagnosisFromDataToView();
                                _ViewCollection.BlocksNames = null;
                                _ViewCollection.LinesNames = null;
                                break;
                            }
                        }
                    }
                }
                if (FocusedDataGrid == "dgBlocksNames" && MultiBox.Equals(_ViewCollection.CurrentBlock))
                {
                    foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
                    {
                        for (int i = 0; i < diagnosis.Blocks.Count; i++)
                        {
                            if (diagnosis.Blocks[i].Name.Equals(MultiBox))
                            {
                                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с названием: \"{MultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    diagnosis.Blocks.RemoveAt(i);
                                    _ViewCollectionProvider.BlocksFromDataToView();
                                    _ViewCollection.LinesNames = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (FocusedDataGrid == "dgLinesContent" && MultiBox.Equals(_ViewCollection.CurrentLine))
                {
                    foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
                    {
                        foreach (Block block in diagnosis.Blocks)
                        {
                            for (int i = 0; i < block.Lines.Count; i++)
                            {
                                if(block.Lines[i].Equals(MultiBox))
                                {
                                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с названием: \"{MultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
                                    if (result == MessageBoxResult.Yes)
                                    {
                                        block.Lines.RemoveAt(i);
                                        _ViewCollectionProvider.LinesFromDataToView();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CanRemoveElementCommandExecute(object parameter) => true;
        
        #endregion

        #endregion
    }
}
