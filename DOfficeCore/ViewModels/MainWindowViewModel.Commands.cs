using System;
using DOfficeCore.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
            _ViewCollectionProvider.DiagnosisFromDataToView(CurrentData);
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
                    _ViewCollectionProvider.BlocksFromDataToView(CurrentData);
                }
                else if (datagrid.Name == "dgBlocksNames")
                {
                    CurrentData.CurrentBlock = (string)datagrid.CurrentItem;
                    _ViewCollectionProvider.LinesFromDataToView(CurrentData);
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
                    _ViewCollectionProvider.DiagnosisFromDataToView(CurrentData);
                }
                if (FocusedDataGrid == "dgBlocksNames" && !MultiBox.Equals(CurrentData.CurrentBlock))
                {
                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(CurrentData.CurrentBlock)).Name = MultiBox;
                    _ViewCollectionProvider.BlocksFromDataToView(CurrentData);
                }
                if (FocusedDataGrid == "dgLinesContent" && !MultiBox.Equals(CurrentData.CurrentLine))
                {
                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(CurrentData.CurrentBlock)).
                        Lines.Remove(CurrentData.CurrentLine);

                    CurrentData.DataCollection.Find(t => t.Code.Equals(CurrentData.CurrentDiagnosis)).
                        Blocks.Find(i => i.Name.Equals(CurrentData.CurrentBlock)).
                        Lines.Add(MultiBox);

                    _ViewCollectionProvider.LinesFromDataToView(CurrentData);
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
                CurrentData.LinesNames = null;
                CurrentData.LinesNames = new ObservableCollection<string>();
                foreach (var line in from Diagnosis diagnosis in CurrentData.DataCollection
                                     from Block block in diagnosis.Blocks
                                     from string line in block.Lines
                                     where line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
                                     select line)
                {
                    CurrentData.LinesNames.Add(line);
                }
            }
            else if (MultiBox != null)
            {
                _ViewCollectionProvider.DiagnosisFromDataToView(CurrentData);
                _ViewCollectionProvider.BlocksFromDataToView(CurrentData);
                _ViewCollectionProvider.LinesFromDataToView(CurrentData);
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
                if (FocusedDataGrid == "dgCodes" && MultiBox.Equals(CurrentData.CurrentDiagnosis))
                {
                    for (int i = 0; i < CurrentData.DataCollection.Count; i++)
                    {
                        if (CurrentData.DataCollection[i].Code.Equals(MultiBox))
                        {
                            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с названием: \"{MultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                CurrentData.DataCollection.RemoveAt(i);
                                _ViewCollectionProvider.DiagnosisFromDataToView(CurrentData);
                                CurrentData.BlocksNames = null;
                                CurrentData.LinesNames = null;
                                break;
                            }
                        }
                    }
                }
                if (FocusedDataGrid == "dgBlocksNames" && MultiBox.Equals(CurrentData.CurrentBlock))
                {
                    foreach (Diagnosis diagnosis in CurrentData.DataCollection)
                    {
                        for (int i = 0; i < diagnosis.Blocks.Count; i++)
                        {
                            if (diagnosis.Blocks[i].Name.Equals(MultiBox))
                            {
                                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с названием: \"{MultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    diagnosis.Blocks.RemoveAt(i);
                                    _ViewCollectionProvider.BlocksFromDataToView(CurrentData);
                                    CurrentData.LinesNames = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (FocusedDataGrid == "dgLinesContent" && MultiBox.Equals(CurrentData.CurrentLine))
                {
                    foreach (Diagnosis diagnosis in CurrentData.DataCollection)
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
                                        _ViewCollectionProvider.LinesFromDataToView(CurrentData);
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
