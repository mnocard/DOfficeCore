using DOfficeCore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;

namespace DOfficeCore.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Форматы открываемых файлов
        private const string dlgFilter = "Word documents (.docx)|*.docx";
        private const string dlgDefaultExt = ".docx";
        #endregion

        #region Свойства окна обработчика строк

        #region TextForEditing : string - Текст, который необходимо обработать

        /// <summary>Текст, который необходимо обработать</summary>
        private string _TextForEditing;

        /// <summary>Текст, который необходимо обработать</summary>
        public string TextForEditing
        {
            get => _TextForEditing;
            set => Set(ref _TextForEditing, value);
        }

        #endregion

        #region RawLines : ObservableCollection<string> - Необработанная коллекция строк

        /// <summary>Необработанная коллекция строк</summary>
        private ObservableCollection<string> _RawLines;

        /// <summary>Необработанная коллекция строк</summary>
        public ObservableCollection<string> RawLines
        {
            get => _RawLines;
            set => Set(ref _RawLines, value);
        }

        #endregion

        #region DiagnosisMultiBox : string - Поле ввода диагноза

        /// <summary>Поле ввода диагноза</summary>
        private string _DiagnosisMultiBox;

        /// <summary>Поле ввода диагноза</summary>
        public string DiagnosisMultiBox
        {
            get => _DiagnosisMultiBox;
            set => Set(ref _DiagnosisMultiBox, value);
        }

        #endregion

        #region BlockMultiBox : string - Поле ввода разделов

        /// <summary>Поле ввода разделов</summary>
        private string _BlockMultiBox;

        /// <summary>Поле ввода разделов</summary>
        public string BlockMultiBox
        {
            get => _BlockMultiBox;
            set => Set(ref _BlockMultiBox, value);
        }

        #endregion

        #region LineMultiBox : string - Поле ввода предложений

        /// <summary>Поле ввода предложений</summary>
        private string _LineMultiBox;

        /// <summary>Поле ввода предложений</summary>
        public string LineMultiBox
        {
            get => _LineMultiBox;
            set => Set(ref _LineMultiBox, value);
        }

        #endregion

        #endregion

        #region Команды

        #region Открытие файла
        /// <summary>Открытие файла</summary>
        public ICommand OpenFileCommand { get; }
        /// <summary>Открытие файла</summary>
        private void OnOpenFileCommandExecuted(object parameter)
        {
            Status = "Открываем файл";
            var dlg = new OpenFileDialog
            {
                DefaultExt = dlgDefaultExt,
                Filter = dlgFilter
            };

            var result = dlg.ShowDialog();

            if (result == true)
            {
                var task = Task.Run(() =>
                {
                    TextForEditing = _LineEditorService.OpenDocument(dlg.FileName);
                    var resultLines = _LineEditorService.TextToLines(TextForEditing);
                    RawLines = new ObservableCollection<string>(resultLines);
                }).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Status = "Что-то пошло не так!";
                        MessageBox.Show("Невозможно открыть файл!\n" + task.Exception.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        private bool CanOpenFileCommandExecute(object parameter) => true;

        #endregion

        #region Получение текста из буфера обмена
        /// <summary>Получение текста из буфера обмена</summary>
        public ICommand GetTextFromClipboardCommand { get; }
        /// <summary>Получение текста из буфера обмена</summary>
        private void OnGetTextFromClipboardCommandExecuted(object parameter)
        {
            Status = "Обрабатываем текст из буфера обмена";

            if (Clipboard.ContainsText())
            {
                TextForEditing = Clipboard.GetText();
                if (RawLines == null) RawLines = new ObservableCollection<string>();
                try
                {
                    foreach (var item in _LineEditorService.TextToLines(TextForEditing))
                    {
                        RawLines.Add(item);
                    }
                Status = "Готово";
                }
                catch (ArgumentNullException e)
                {
                    Status = "Что-то пошло не так!";
                    MessageBox.Show("Что-то пошло не так!\n" + e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else Status = "Буфер обмена пуст";
        }

        private bool CanGetTextFromClipboardCommandExecute(object parameter) => true;

        #endregion

        #region Сохранение данных в файл
        /// <summary>Сохранение данных в файл</summary>
        public ICommand SaveDataToFileCommand { get; }
        /// <summary>Сохранение данных в файл</summary>
        private void OnSaveDataToFileCommandExecuted(object p)
        {
            if (DataCollection != null)
            {
                _DataProviderService.SaveDataToFile(DataCollection, "lines");
                Status = "Ваша коллекция сохраненая";
            }
            else Status = "Нечего сохранять";
        }

        private bool CanSaveDataToFileCommandExecute(object p) => true;
        #endregion

        #region Очистка необработанной таблицы
        /// <summary>Очистка необработанной таблицы</summary>
        public ICommand ClearListBoxCommand { get; }
        /// <summary>Очистка необработанной таблицы</summary>
        private void OnClearListBoxCommandExecuted(object parameter)
        {
            RawLines = null;
            Status = "Таблица предложений очищена";
        }

        private bool CanClearListBoxCommandExecute(object parameter) => RawLines != null;

        #endregion

        #region Перенос строки из необработанной коллекции
        /// <summary>Перенос строки из необработанной коллекции</summary>
        public ICommand TransferCommand { get; }
        /// <summary>Перенос строки из необработанной коллекции</summary>
        private void OnTransferCommandExecuted(object parameter)
        {
            if (parameter is ListBox listBox)
            {
                if (listBox.Name.Equals("lbRawLines") && CurrentSection != null)
                {
                    _ViewCollectionProvider.AddLine(DataCollection, CurrentSection, (string)listBox.SelectedItem);
                    LinesList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, CurrentSection);
                    RawLines.Remove((string)listBox.SelectedItem);
                    Status = "Выбранная строка перемещена в таблицу предложений";
                }
                else if (listBox.SelectedItem is Section CurrentItem)
                {
                    CurrentSection = CurrentItem;
                    switch (listBox.Name)
                    {
                        case "dgDiagnosisLineEditView":
                            BlocksList = _ViewCollectionProvider.BlocksFromDataToView(DataCollection, CurrentSection);
                            LinesList = new ObservableCollection<Section>();
                            DiagnosisMultiBox = CurrentSection.Diagnosis;
                            BlockMultiBox = null;
                            LineMultiBox = null;
                            break;
                        case "dgBlocksLineEditView":
                            LinesList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, CurrentSection);
                            BlockMultiBox = CurrentSection.Block;
                            LineMultiBox = null;
                            break;
                        case "dgLinesLineEditView":
                            LineMultiBox = CurrentSection.Line;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private bool CanTransferCommandExecute(object parameter) => true;

        #endregion

        #region Добавление

        #region Добавление нового диагноза в коллекцию
        /// <summary>Добавление нового диагноза в коллекцию</summary>
        public ICommand AddDiagnosisCommand { get; }
        /// <summary>Добавление нового диагноза в коллекцию</summary>
        private void OnAddDiagnosisCommandExecuted(object parameter)
        {
            if (DataCollection == null) DataCollection = new List<Section>();
            if (DiagnosisMultiBox != null)
            {
                _ViewCollectionProvider.AddDiagnosis(DataCollection, DiagnosisMultiBox);
                DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
                Status = "Добавлен диагноз " + DiagnosisMultiBox;
            }
            else Status = "Нечего добавлять";
        }

        private bool CanAddDiagnosisCommandExecute(object parameter) => true;

        #endregion

        #region Добавление раздела в коллекцию
        /// <summary>Добавление раздела в коллекцию</summary>
        public ICommand AddBlockCommand { get; }
        /// <summary>Добавление раздела в коллекцию</summary>
        private void OnAddBlockCommandExecuted(object parameter)
        {
            if (DataCollection != null && CurrentSection != null && BlockMultiBox != null)
            {
                _ViewCollectionProvider.AddBlock(DataCollection, CurrentSection, BlockMultiBox);
                BlocksList = _ViewCollectionProvider.BlocksFromDataToView(DataCollection, CurrentSection);
                Status = "Добавлен раздел " + BlockMultiBox + " в диагноз " + CurrentSection.Diagnosis;
            }
            else Status = "Нечего добавлять";
        }

        private bool CanAddBlockCommandExecute(object parameter) => true;

        #endregion

        #region Добавление нового предложения в коллекцию
        /// <summary>Добавление нового предложения в коллекцию</summary>
        public ICommand AddLineCommand { get; }
        /// <summary>Добавление нового предложения в коллекцию</summary>
        private void OnAddLineCommandExecuted(object parameter)
        {
            if (DataCollection != null && CurrentSection != null && LineMultiBox != null)
            {
                _ViewCollectionProvider.AddLine(DataCollection, CurrentSection, LineMultiBox);
                LinesList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, CurrentSection);
                Status = "Добавлено предложение в раздел " + CurrentSection.Block + " в диагнозе " + CurrentSection.Diagnosis;
            }
            else Status = "Нечего добавлять";
        }

        private bool CanAddLineCommandExecute(object parameter) => true;

        #endregion

        #endregion

        #region Редактирование

        #region Редактирование названия диагноза
        /// <summary>Редактирование названия диагноза</summary>
        public ICommand EditDiagnosisCommand { get; }
        /// <summary>Редактирование названия диагноза</summary>
        private void OnEditDiagnosisCommandExecuted(object parameter)
        {
            if (DataCollection != null && CurrentSection != null && DiagnosisMultiBox != null)
            {
                _ViewCollectionProvider.EditDiagnosis(DataCollection, CurrentSection, DiagnosisMultiBox);
                Status = "Диагноз " + CurrentSection.Diagnosis + " переименован в " + DiagnosisMultiBox;
            }
            else Status = "Нечего редактировать";
        }

        private bool CanEditDiagnosisCommandExecute(object parameter) => true;

        #endregion

        #region Редактирование названия раздела
        /// <summary>Редактирование названия раздела</summary>
        public ICommand EditBlockCommand { get; }
        /// <summary>Редактирование названия раздела</summary>
        private void OnEditBlockCommandExecuted(object parameter)
        {
            if (DataCollection != null && CurrentSection != null && BlockMultiBox != null)
            {
                _ViewCollectionProvider.EditBlock(DataCollection, CurrentSection, BlockMultiBox);
                Status = "Раздел " + CurrentSection.Block + " переименован в " + BlockMultiBox;
            }
            else Status = "Нечего редактировать";
        }

        private bool CanEditBlockCommandExecute(object parameter) => true;

        #endregion

        #region Редактирование строки
        /// <summary>Редактирование строки</summary>
        public ICommand EditLineCommand { get; }
        /// <summary>Редактирование строки</summary>
        private void OnEditLineCommandExecuted(object parameter)
        {
            if (DataCollection != null && CurrentSection != null && LineMultiBox != null)
            {
                _ViewCollectionProvider.EditLine(DataCollection, CurrentSection, LineMultiBox);
                Status = "Предложение изменено";
            }
            else Status = "Нечего редактировать";
        }

        private bool CanEditLineCommandExecute(object parameter) => true;

        #endregion

        #endregion

        #region Удаление

        #region Удаление диагноза
        /// <summary>Удаление диагноза</summary>
        public ICommand RemoveDiagnosisCommand { get; }
        /// <summary>Удаление диагноза</summary>
        private void OnRemoveDiagnosisCommandExecuted(object parameter)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить диагноз \"{DiagnosisMultiBox}\"? Все элементы, относящиеся к этому диагнозу также будут удалены!", "Внимание!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes && DataCollection != null && CurrentSection != null)
            {
                _ViewCollectionProvider.RemoveDiagnosis(DataCollection, CurrentSection);
                DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
                BlocksList = null;
                LinesList = null;
                Status = "Удален диагноз " + CurrentSection.Diagnosis;
                CurrentSection = null;
            }
            else Status = "Удаление отменено";
        }

        private bool CanRemoveDiagnosisCommandExecute(object parameter) => true;

        #endregion

        #region Удаление раздела
        /// <summary>Удаление раздела</summary>
        public ICommand RemoveBlockCommand { get; }
        /// <summary>Удаление раздела</summary>
        private void OnRemoveBlockCommandExecuted(object parameter)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить раздел \"{BlockMultiBox}\"? Все элементы также относящиеся к этому разделу также будут удалены!", "Внимание!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes && DataCollection != null && CurrentSection != null)
            {
                _ViewCollectionProvider.RemoveBlock(DataCollection, CurrentSection);
                BlocksList = _ViewCollectionProvider.BlocksFromDataToView(DataCollection, CurrentSection);
                LinesList = null;
                Status = "Удален раздел " + CurrentSection.Block;
                CurrentSection = null;
            }
            else Status = "Удаление отменено";
        }

        private bool CanRemoveBlockCommandExecute(object parameter) => true;

        #endregion

        #region Удаление строки
        /// <summary>Удаление строки</summary>
        public ICommand RemoveLineCommand { get; }
        /// <summary>Удаление строки</summary>
        private void OnRemoveLineCommandExecuted(object parameter)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить строку \"{LineMultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes && DataCollection != null && CurrentSection != null)
            {
                _ViewCollectionProvider.RemoveLine(DataCollection, CurrentSection);
                LinesList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, CurrentSection);
                Status = "Удалено предложение";
                CurrentSection = null;
            }
            else Status = "Удаление отменено";
        }

        private bool CanRemoveLineCommandExecute(object parameter) => true;

        #endregion

        #endregion

        #region Возврат строки в необработанную коллекцию
        /// <summary>Возврат строки в необработанную коллекцию</summary>
        public ICommand ReturnLineCommand { get; }
        /// <summary>Возврат строки в необработанную коллекцию</summary>
        private void OnReturnLineCommandExecuted(object parameter)
        {
            if (RawLines == null) RawLines = new ObservableCollection<string>();
            if (LineMultiBox != null)
            {
                _ViewCollectionProvider.RemoveLine(DataCollection, CurrentSection);
                LinesList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
                RawLines.Add(CurrentSection.Line);
                CurrentSection = null;
                Status = "Выбранное предложение было возвращено в таблицу предложений";
            }
        }

        private bool CanReturnLineCommandExecute(object parameter) => true;

        #endregion

        #endregion
    }
}
