using DOfficeCore.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System;

using System.Threading.Tasks;
using Serilog;
using System.IO;
using System.Linq;

namespace DOfficeCore.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Форматы открываемых файлов
        private const string dlgFilter = "Word documents (.docx)|*.docx";
        private const string dlgDefaultExt = ".docx";
        private const string dataFileName = "Data";
        private const string dataFileFormat = ".json";
        private const string dataFileFilter = "json documents (.json)|*.json";
        #endregion

        #region Свойства окна обработчика строк

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

        #region SectorsMultiBox : string - Поле ввода диагноза

        /// <summary>Поле ввода диагноза</summary>
        private string _SectorsMultiBox;

        /// <summary>Поле ввода диагноза</summary>
        public string SectorsMultiBox
        {
            get => _SectorsMultiBox;
            set => Set(ref _SectorsMultiBox, value);
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

        #region SelectedSector : Sector - Выбранный сектор

        /// <summary>Выбранный сектор</summary>
        private Sector _SelectedSector;

        /// <summary>Выбранный сектор</summary>
        public Sector SelectedSector
        {
            get => _SelectedSector;
            set => Set(ref _SelectedSector, value);
        }

        #endregion

        #region SelectedBlock : Block - Выбранный блок

        /// <summary>Выбранный блок</summary>
        private Block _SelectedBlock;

        /// <summary>Выбранный блок</summary>
        public Block SelectedBlock
        {
            get => _SelectedBlock;
            set => Set(ref _SelectedBlock, value);
        }

        #endregion

        #region SelectedLine : string - Выбранная строка

        /// <summary>Выбранная строка</summary>
        private string _SelectedLine;

        /// <summary>Выбранная строка</summary>
        public string SelectedLine
        {
            get => _SelectedLine;
            set => Set(ref _SelectedLine, value);
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

            if (dlg.ShowDialog() is true)
            {
                var task = Task.Run(() =>
                {
                    var resultLines = _LineEditorService.OpenAndConvert(dlg.FileName);
                    RawLines = new(resultLines);
                }).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Log.Error("Невозможно открыть файл!\n" + task.Exception.Message);
                        Status = "Что-то пошло не так!";
                        MessageBox.Show("Невозможно открыть файл!\n" + task.Exception.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

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
                var textForEditing = Clipboard.GetText();

                if (!string.IsNullOrWhiteSpace(textForEditing))
                {
                    try
                    {
                        var resultLines = _LineEditorService.TextToLines(textForEditing);
                        RawLines = new(RawLines.Concat(resultLines));
                        Status = "Готово";
                    }
                    catch (ArgumentNullException e)
                    {
                        Log.Error("Ошибка копирования текста из буфера обмена. ArgumentNullException." + e.Message);
                        Status = "Что-то пошло не так!";
                        MessageBox.Show("Что-то пошло не так!\n" + e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else Status = "Буфер обмена пуст";
        }
        #endregion

        #region Сохранение данных в файл
        /// <summary>Сохранение данных в файл</summary>
        public ICommand SaveDataToFileCommand { get; }
        /// <summary>Сохранение данных в файл</summary>
        private void OnSaveDataToFileCommandExecuted(object p)
        {
            if (SectorsCollection.Any())
            {
                if (!Directory.Exists(_Folder))
                    Directory.CreateDirectory(_Folder);

                var dlg = new SaveFileDialog
                {
                    InitialDirectory = _Folder,
                    FileName = dataFileName,
                    DefaultExt = dataFileFormat,
                    Filter = dataFileFilter
                };

                if (dlg.ShowDialog() is true)
                {
                    if (_DataProviderService.SaveDataToFile(SectorsCollection, dlg.FileName))
                        Status = "Ваша коллекция сохраненая";
                    else Status = "Непредвиденная ошибка! Сохранение не удалось";
                }
                else Status = "Ну и не надо. Больно-то и хотелось.";
            }
            else Status = "Нечего сохранять";
        }
        #endregion

        #region Загрузка бд из файла
        /// <summary>Загрузка бд из файла</summary>
        public ICommand LoadDataFromFileCommand { get; }
        /// <summary>Загрузка бд из файла</summary>
        private void OnLoadDataFromFileCommandExecuted(object parameter)
        {
            var confirmDlg = MessageBox.Show(
                "Вы хотите добавить новые данные к существующей коллекции или удалить старые данные и оставить только новые? " +
                "Учтите, что, если старая коллекция не сохранена, то она будет утрачена безвозвратно!" +
                "\nДа - объединить старую коллекцию и новую" +
                "\nНет - оставить только новую коллекцию" +
                "\nОтмена - оставить всё как есть", "Внимание!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (confirmDlg.Equals(MessageBoxResult.Cancel))
                return;

            var dlg = new OpenFileDialog
            {
                InitialDirectory = _Folder,
                FileName = dataFileName,
                DefaultExt = dataFileFormat,
                Filter = dataFileFilter
            };

            if (dlg.ShowDialog() is true)
            {
                var newCollection = _DataProviderService.LoadSectorsFromFile(Path.Combine(_Folder, dlg.FileName));

                if (confirmDlg.Equals(MessageBoxResult.Yes))
                    SectorsCollection = new(SectorsCollection.Concat(newCollection));
                else SectorsCollection = new(newCollection);
                RefreshSectors();
            }
            else Status = "Ну и не надо. Больно-то и хотелось.";
        }
        #endregion

        #region Очистка необработанной таблицы
        /// <summary>Очистка необработанной таблицы</summary>
        public ICommand ClearListBoxCommand { get; }
        /// <summary>Очистка необработанной таблицы</summary>
        private void OnClearListBoxCommandExecuted(object parameter)
        {
            RawLines = new();
            Status = "Таблица предложений очищена";
        }
        #endregion

        #region Удалить элемент из таблицы
        /// <summary>Удалить элемент из таблицы</summary>
        public ICommand RemoveElementFromRawCommand { get; }
        /// <summary>Удалить элемент из таблицы</summary>
        private void OnRemoveElementFromRawCommandExecuted(object parameter)
        {
            if (SelectedLine is not null &&
                RawLines is not null)
                RawLines.Remove(SelectedLine);
        }
        #endregion

        #region Щелчок по элементу списка необработанных строк
        /// <summary>Щелчок по элементу списка необработанных строк</summary>
        public ICommand SelectedRawLineCommand { get; }
        /// <summary>Щелчок по элементу списка необработанных строк</summary>
        private void OnSelectedRawLineCommandExecuted(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SelectedLine)
                && SelectedBlock != null)
            {
                if (_NewCollectionHandler.AddLine(SectorsCollection, SelectedBlock, SelectedLine))
                {
                    RefreshLines();
                    RawLines.Remove(SelectedLine);
                    Status = "Выбранная строка перемещена в таблицу предложений";
                }
            }
        }
        #endregion

        #region Щелчок по элементу списка диагнозов в окне редактирования строк
        /// <summary>Щелчок по элементу списка диагнозов в окне редактирования строк</summary>
        public ICommand SelectedDiagnosisELCommand { get; }
        /// <summary>Щелчок по элементу списка диагнозов в окне редактирования строк</summary>
        private void OnSelectedDiagnosisELCommandExecuted(object parameter)
        {
            if (SectorsList.Any())
            {
                RefreshBlocks();
                LinesList = new();

                SectorsMultiBox = SelectedSector.Name;
                BlockMultiBox = null;
                LineMultiBox = null;
            }
        }
        #endregion

        #region Щелчок по элементу списка блоков в окне редактирования строк
        /// <summary>Щелчок по элементу списка блоков в окне редактирования строк</summary>
        public ICommand SelectedBlockELCommand { get; }
        /// <summary>Щелчок по элементу списка блоков в окне редактирования строк</summary>
        private void OnSelectedBlockELCommandExecuted(object parameter)
        {
            if (SectorsList.Any() &&
                BlocksList.Any())
            {
                RefreshLines();
                BlockMultiBox = SelectedBlock.Name;
                LineMultiBox = null;
            }
        }
        #endregion

        #region Щелчок по элементу списка строк в окне редактирования строк
        /// <summary>Щелчок по элементу списка строк в окне редактирования строк</summary>
        public ICommand SelectedLinesELCommand { get; }
        /// <summary>Щелчок по элементу списка строк в окне редактирования строк</summary>
        private void OnSelectedLinesELCommandExecuted(object parameter)
        {
            if(SectorsList.Any() &&
                BlocksList.Any() &&
                LinesList.Any())
            LineMultiBox = SelectedLine;
        }
        #endregion

        #region Добавление

        #region Добавление нового диагноза в коллекцию
        /// <summary>Добавление нового диагноза в коллекцию</summary>
        public ICommand AddDiagnosisCommand { get; }
        /// <summary>Добавление нового диагноза в коллекцию</summary>
        private void OnAddDiagnosisCommandExecuted(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SectorsMultiBox))
            {
                if (SectorsCollection is null) SectorsCollection = new();
                
                if (_NewCollectionHandler.AddSector(SectorsCollection, SectorsMultiBox))
                {
                    RefreshSelectedSector();
                    RefreshSectors();
                    RefreshBlocks();
                    LinesList = new();

                    Status = "Добавлен элемент " + SectorsMultiBox;
                }
                else Status = "Такой элемент уже существует.";
            }
            else Status = "Нечего добавлять";
        }
        #endregion

        #region Добавление раздела в коллекцию
        /// <summary>Добавление раздела в коллекцию</summary>
        public ICommand AddBlockCommand { get; }
        /// <summary>Добавление раздела в коллекцию</summary>
        private void OnAddBlockCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                SelectedSector is not null &&
                !string.IsNullOrWhiteSpace(BlockMultiBox))
            {
                if (_NewCollectionHandler.AddBlock(SectorsCollection, SelectedSector, BlockMultiBox))
                {
                    RefreshSelectedBlock();
                    RefreshBlocks();
                    RefreshLines();
                    Status = "Добавлен элемент " + SectorsMultiBox;
                }
                else Status = "Такой элемент уже существует.";
            }
            else Status = "Нечего добавлять";
        }
        #endregion

        #region Добавление нового предложения в коллекцию
        /// <summary>Добавление нового предложения в коллекцию</summary>
        public ICommand AddLineCommand { get; }
        /// <summary>Добавление нового предложения в коллекцию</summary>
        private void OnAddLineCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                SelectedBlock is not null &&
                !string.IsNullOrWhiteSpace(LineMultiBox))
            {
                if (_NewCollectionHandler.AddLine(SectorsCollection, SelectedBlock, LineMultiBox))
                {
                    SelectedLine = LineMultiBox;
                    Status = "Добавлен элемент " + SectorsMultiBox;
                    RefreshLines();
                }
                else Status = "Такой элемент уже существует.";
            }
            else Status = "Нечего добавлять";
        }
        #endregion

        #endregion

        #region Редактирование

        #region Редактирование названия диагноза
        /// <summary>Редактирование названия диагноза</summary>
        public ICommand EditDiagnosisCommand { get; }
        /// <summary>Редактирование названия диагноза</summary>
        private void OnEditDiagnosisCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                SelectedSector is not null &&
               !string.IsNullOrWhiteSpace(SectorsMultiBox))
            {
                if (_NewCollectionHandler.EditSector(SectorsCollection, SelectedSector, SectorsMultiBox))
                {
                    RefreshSelectedSector();
                    RefreshSectors();
                    RefreshBlocks();
                    LinesList = new();

                    Status = "Сектор переименован в " + SectorsMultiBox;
                }
            }
            else Status = "Нечего редактировать";
        }
        #endregion

        #region Редактирование названия раздела
        /// <summary>Редактирование названия раздела</summary>
        public ICommand EditBlockCommand { get; }
        /// <summary>Редактирование названия раздела</summary>
        private void OnEditBlockCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                SelectedBlock is not null &&
                !string.IsNullOrWhiteSpace(BlockMultiBox))
            {
                if (_NewCollectionHandler.EditBlock(SectorsCollection, SelectedBlock, BlockMultiBox))
                {
                    RefreshSelectedBlock();
                    RefreshBlocks();
                    RefreshLines();

                    Status = "Раздел переименован в " + BlockMultiBox;
                }
            }
            else Status = "Нечего редактировать";
        }
        #endregion

        #region Редактирование строки
        /// <summary>Редактирование строки</summary>
        public ICommand EditLineCommand { get; }
        /// <summary>Редактирование строки</summary>
        private void OnEditLineCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
               !string.IsNullOrWhiteSpace(SelectedLine) &&
               !string.IsNullOrWhiteSpace(LineMultiBox))
            {
                if (_NewCollectionHandler.EditLine(SectorsCollection, SelectedBlock, SelectedLine, LineMultiBox))
                {
                    RefreshLines();
                    Status = "Предложение изменено";
                }
            }
            else Status = "Нечего редактировать";
        }
        #endregion

        #endregion

        #region Удаление

        #region Удаление диагноза
        /// <summary>Удаление диагноза</summary>
        public ICommand RemoveDiagnosisCommand { get; }
        /// <summary>Удаление диагноза</summary>
        private void OnRemoveDiagnosisCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                SelectedSector is not null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить сектор \"{SectorsMultiBox}\"? " +
                    $"Все элементы, относящиеся к этому диагнозу также будут удалены!", "Внимание!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (_NewCollectionHandler.RemoveSector(SectorsCollection, SelectedSector))
                    {
                        RefreshSectors();
                        BlocksList = new();
                        LinesList = new();

                        Status = "Удален сектор " + SectorsMultiBox;
                        SectorsMultiBox = null;
                    }
                }
                else Status = "Удаление отменено";
            }
            else Status = "Выберите элемент, который хотите удалить";
        }
        #endregion

        #region Удаление раздела
        /// <summary>Удаление раздела</summary>
        public ICommand RemoveBlockCommand { get; }
        /// <summary>Удаление раздела</summary>
        private void OnRemoveBlockCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                SelectedBlock is not null)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить раздел \"{BlockMultiBox}\"? " +
                    $"Все элементы также относящиеся к этому разделу также будут удалены!", "Внимание!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (_NewCollectionHandler.RemoveBlock(SectorsCollection, SelectedSector, SelectedBlock))
                    {
                        RefreshBlocks();
                        LinesList = new();

                        Status = "Удален раздел " + BlockMultiBox;
                        BlockMultiBox = null;
                    }
                }
                else Status = "Удаление отменено";
            }
            else Status = "Выберите элемент, который хотите удалить";
        }
        #endregion

        #region Удаление строки
        /// <summary>Удаление строки</summary>
        public ICommand RemoveLineCommand { get; }
        /// <summary>Удаление строки</summary>
        private void OnRemoveLineCommandExecuted(object parameter)
        {
            if (SectorsCollection is not null &&
                !string.IsNullOrWhiteSpace(SelectedLine))
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить строку \"{LineMultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                    if (_NewCollectionHandler.RemoveLine(SectorsCollection, SelectedBlock, SelectedLine))
                    {
                        RefreshLines();
                        Status = "Удалено предложение";
                        LineMultiBox = null;
                    }
                    else Status = "Удаление отменено";
            }
            else Status = "Выберите элемент, который хотите удалить";
        }
        #endregion

        #endregion

        #region Возврат строки в необработанную коллекцию
        /// <summary>Возврат строки в необработанную коллекцию</summary>
        public ICommand ReturnLineCommand { get; }
        /// <summary>Возврат строки в необработанную коллекцию</summary>
        private void OnReturnLineCommandExecuted(object parameter)
        {
            if (RawLines is null) RawLines = new();
            if (!string.IsNullOrWhiteSpace(SelectedLine))
            {
                RawLines.Add(SelectedLine);

                if (_NewCollectionHandler.RemoveLine(SectorsCollection, SelectedBlock, SelectedLine))
                {
                    RefreshLines();
                    SelectedLine = null;
                    Status = "Выбранное предложение было возвращено в таблицу предложений";
                }
            }
        }
        #endregion

        #region Изменение индекса элемента

        #region Смещение сектора в списке вверх
        /// <summary>Смещение сектора в списке вверх</summary>
        public ICommand SectorIndexUpCommand { get; }
        /// <summary>Смещение сектора в списке вверх</summary>
        private void OnSectorIndexUpCommandExecuted(object parameter)
        {
            if (SelectedSector is not null)
            {
                var index = SectorsCollection.IndexOf(SelectedSector);
                if (index > 0)
                {
                    SectorsCollection.Insert(index - 1, SelectedSector);
                    SectorsCollection.RemoveAt(index + 1);
                    RefreshSectors();
                }
            }
        }
        #endregion

        #region Смещение сектора в списке вниз
        /// <summary>Смещение сектора в списке вниз</summary>
        public ICommand SectorIndexDownCommand { get; }
        /// <summary>Смещение сектора в списке вниз</summary>
        private void OnSectorIndexDownCommandExecuted(object parameter)
        {
            if (SelectedSector is not null)
            {
                var index = SectorsCollection.IndexOf(SelectedSector);
                if (index < SectorsCollection.Count - 1)
                {
                    SectorsCollection.Insert(index + 2, SelectedSector);
                    SectorsCollection.RemoveAt(index);
                    RefreshSectors();
                }
            }
        }
        #endregion

        #region Смещение блока в списке вверх
        /// <summary>Смещение блока в списке вверх</summary>
        public ICommand BlockIndexUpCommand { get; }
        /// <summary>Смещение блока в списке вверх</summary>
        private void OnBlockIndexUpCommandExecuted(object parameter)
        {
            if (SelectedBlock is not null)
            {
                var index = GetIndexOfBlock();
                if (index > 0)
                {
                    InsertBlock(index - 1);
                    RemoveBlock(index + 1);
                    RefreshBlocks();
                }
            }
        }
        #endregion

        #region Смещение блока в списке вниз
        /// <summary>Смещение блока в списке вниз</summary>
        public ICommand BlockIndexDownCommand { get; }
        /// <summary>Смещение блока в списке вниз</summary>
        private void OnBlockIndexDownCommandExecuted(object parameter)
        {
            if (SelectedBlock is not null)
            {
                var index = GetIndexOfBlock();
                if (index < GetBlocksCount() - 1)
                {
                    InsertBlock(index + 2);
                    RemoveBlock(index);
                    RefreshBlocks();
                }
            }
        }
        #endregion

        #region Смещение строки в списке вверх
        /// <summary>Смещение строки в списке вверх</summary>
        public ICommand LineIndexUpCommand { get; }
        /// <summary>Смещение строки в списке вверх</summary>
        private void OnLineIndexUpCommandExecuted(object parameter)
        {
            if (SelectedBlock is not null &&
                SelectedLine is not null)
            {
                var index = GetIndexOfLine();
                if (index > 0)
                {
                    InsertLine(index - 1);
                    RemoveLine(index + 1);
                    RefreshLines();
                }
            }
        }
        #endregion

        #region Смещение строки в списке вниз
        /// <summary>Смещение строки в списке вниз</summary>
        public ICommand LineIndexDownCommand { get; }
        /// <summary>Смещение строки в списке вниз</summary>
        private void OnLineIndexDownCommandExecuted(object parameter)
        {
            if (SelectedBlock is not null &&
                SelectedLine is not null)
            {
                var index = GetIndexOfLine();
                if (index < GetLinesCount() - 1)
                {
                    InsertLine(index + 2);
                    RemoveLine(index);
                    RefreshLines();
                }
            }
        }
        #endregion

        #endregion

        #endregion

        #region Приватные методы
        private void RefreshSectors() => SectorsList = new(SectorsCollection);
        private void RefreshBlocks() => BlocksList = new(_NewViewCollectionProvider.GetBlocks(SectorsCollection, SelectedSector));
        private void RefreshLines() => LinesList = new(_NewViewCollectionProvider.GetLines(SectorsCollection, SelectedBlock));
        private void RefreshSelectedSector() => SelectedSector = SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SectorsMultiBox));
        private void RefreshSelectedBlock() => SelectedBlock = SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedSector.Name)).Blocks.FirstOrDefault(block =>
                                                block.Name.Equals(BlockMultiBox));
        private int GetIndexOfBlock() => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.IndexOf(SelectedBlock);
        private int GetBlocksCount() => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.Count;
        private void InsertBlock(int index) => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.Insert(index, SelectedBlock);
        private void RemoveBlock(int index) => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.RemoveAt(index);
        private int GetIndexOfLine() => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.FirstOrDefault(block =>
                                                block.Name.Equals(SelectedBlock.Name)).Lines.IndexOf(SelectedLine);
        private int GetLinesCount() => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.FirstOrDefault(block =>
                                                block.Name.Equals(SelectedBlock.Name)).Lines.Count;
        private void InsertLine(int index) => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.FirstOrDefault(block =>
                                                block.Name.Equals(SelectedBlock.Name)).Lines.Insert(index, SelectedLine);
        private void RemoveLine(int index) => SectorsCollection.FirstOrDefault(sector =>
                                            sector.Name.Equals(SelectedBlock.Sector)).Blocks.FirstOrDefault(block =>
                                                block.Name.Equals(SelectedBlock.Name)).Lines.RemoveAt(index);
        #endregion
    }
}
