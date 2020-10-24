using DOfficeCore.Logger;
using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {
        public ViewCollectionProvider(ILogger Logger)
        {
            _Logger = Logger;
        }

        #region Сервисы
        private readonly ILogger _Logger;
        #endregion

        #region Методы

        #region Получение списков

        /// <summary>
        /// Получение списка диагнозов для модели представления
        /// </summary>
        /// <param name="DataCollection">Коллекция элементов базы данных</param>
        /// <returns>Список диагнозов</returns>
        public ObservableCollection<Section> DiagnosisFromDataToView(IEnumerable<Section> DataCollection)
        {
            _Logger.WriteLog("INFO");

            if (DataCollection == null)
            {
                _Logger.WriteLog("DataCollection is null");
                DataCollection = new HashSet<Section>();
                return new ObservableCollection<Section>();
            }

            var DiagnosisList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (DiagnosisList.Count == 0) DiagnosisList.Add(item);
                else if (!DiagnosisList.Any(t => t.Diagnosis.Equals(item.Diagnosis))) DiagnosisList.Add(item);
            }

            if (DiagnosisList.Count > 0) _Logger.WriteLog("DiagnosisList returned succesfully");
            else _Logger.WriteLog("DiagnosisList is empty");
            return DiagnosisList;
        }

        /// <summary>
        /// Получение списка разделов для модели представления
        /// </summary>
        /// <param name="DataCollection">Коллекция элементов базы данных</param>
        /// <param name="CurrentSection">Выбранная секция базы данных</param>
        /// <returns>Список разделов</returns>
        public ObservableCollection<Section> BlocksFromDataToView(IEnumerable<Section> DataCollection, Section CurrentSection)
        {
            _Logger.WriteLog("INFO");

            if (CurrentSection.Block == null)
            {
                _Logger.WriteLog("Current section is null");
                return new ObservableCollection<Section>();
            }
            var BlockList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (BlockList.Count == 0 && item.Diagnosis.Equals(CurrentSection.Diagnosis)) BlockList.Add(item);
                else if (item.Diagnosis.Equals(CurrentSection.Diagnosis) && !BlockList.Any(t => t.Block.Equals(item.Block))) BlockList.Add(item);
            }

            if (BlockList.Count > 0) _Logger.WriteLog("BlocksList returned succesfully");
            else _Logger.WriteLog("BlocksList is empty");
            return BlockList;
        }

        /// <summary>
        /// Получение списка строк для модели представления
        /// </summary>
        /// <param name="DataCollection">Коллекция элементов базы данных</param>
        /// <param name="CurrentSection">Выбранная секция</param>
        /// <returns>Список строк</returns>
        public ObservableCollection<Section> LinesFromDataToView(IEnumerable<Section> DataCollection, Section CurrentSection)
        {
            _Logger.WriteLog("INFO");

            if (CurrentSection.Line == null)
            {
                _Logger.WriteLog("Current section is null");
                return new ObservableCollection<Section>();
            }

            var LineList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (LineList.Count == 0 && item.Block.Equals(CurrentSection.Block) && item.Diagnosis.Equals(CurrentSection.Diagnosis)) LineList.Add(item);
                else if (item.Block.Equals(CurrentSection.Block) &&
                    item.Diagnosis.Equals(CurrentSection.Diagnosis)) LineList.Add(item);
            }

            if (LineList.Count > 0) _Logger.WriteLog("LineList returned succesfully");
            else _Logger.WriteLog("LineList is empty");
            return LineList;
        }
        #endregion

        #region Удаление элементов

        /// <summary>
        /// Удаление всех элементов диагноза
        /// </summary>
        /// <param name="DataCollection">Коллекция элемента</param>
        /// <param name="CurrentSection">Секция, из которого берется диагноз</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveDiagnosis(HashSet<Section> DataCollection, Section CurrentSection)
        {
            _Logger.WriteLog("INFO");

            var result = DataCollection.RemoveWhere(t => t.Diagnosis.Equals(CurrentSection.Diagnosis));

            if (result > 0)
            {
                _Logger.WriteLog($"({result}) items in diagnosis {CurrentSection.Diagnosis} removed succesfully.");
                return true;
            }
            else
            {
                _Logger.WriteLog("Nothing to remove.");
                return false;
            }
        }

        /// <summary>
        /// Удаление всех элементов раздела
        /// </summary>
        /// <param name="DataCollection">Коллекция из которой происходит удаление</param>
        /// <param name="CurrentSection">Секция, из которой берутся данные о диагнозе и разделе</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveBlock(HashSet<Section> DataCollection, Section CurrentSection)
        {
            _Logger.WriteLog("INFO");

            var result = DataCollection.RemoveWhere(t => t.Diagnosis.Equals(CurrentSection.Diagnosis) && t.Block.Equals(CurrentSection.Block));

            if (result > 0)
            {
                _Logger.WriteLog($"({result}) items in block {CurrentSection.Block} in diagnosis {CurrentSection.Diagnosis} removed succesfully.");
                return true;
            }
            else
            {
                _Logger.WriteLog("Nothing to remove.");
                return false;
            }
        }

        /// <summary>
        /// Удаление строки
        /// </summary>
        /// <param name="DataCollection">Коллекция из которой происходит удаление</param>
        /// <param name="CurrentSection">Секция, содержащая строку</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveLine(HashSet<Section> DataCollection, Section CurrentSection)
        {
            _Logger.WriteLog("INFO");

            var result = DataCollection.Remove(CurrentSection);
            _Logger.WriteLog($"Result of removing element is ({result}).");
            return result;
        }
        #endregion

        #region Переименовывание

        /// <summary>
        /// Переименовывание диагноза
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="CurrentItem">Секция, из которой берется старое название диагноз</param>
        /// <param name="MultiBox">Новое название диагноза</param>
        /// <returns>True если успешно переименовано</returns>
        public bool EditDiagnosis(HashSet<Section> DataCollection, Section CurrentItem, string MultiBox)
        {
            _Logger.WriteLog("INFO");
            bool result = false;

            foreach (Section item in DataCollection)
            {
                if (item.Diagnosis.Equals(CurrentItem.Diagnosis))
                {
                    item.Diagnosis = MultiBox;
                    result = true;
                }
            }

            _Logger.WriteLog($"Trying to change diagnosis code, result: {result}");
            return result;
        }

        /// <summary>
        /// Переименовывание раздела
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="CurrentItem">Секция, из которой берется старое название блока и диагноза, в котором блок находится</param>
        /// <param name="MultiBox">Новое название блока</param>
        /// <returns>True если успешно переименовано</returns>
        public bool EditBlock(HashSet<Section> DataCollection, Section CurrentItem, string MultiBox)
        {
            _Logger.WriteLog("INFO");
            bool result = false;

            foreach (Section item in DataCollection)
            {
                if (item.Diagnosis.Equals(CurrentItem.Diagnosis) && item.Block.Equals(CurrentItem.Block))
                {
                    item.Block = MultiBox;
                    result = true;
                }
            }

            _Logger.WriteLog($"Trying to change block name, result: {result}");
            return result;
        }

        /// <summary>
        /// Переименовывание строки
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="CurrentItem">Секция, из которой берется название блока и диагноза, в которой находится строка</param>
        /// <param name="MultiBox">Новая строка</param>
        /// <returns>True если успешно переименовано</returns>
        public bool EditLine(HashSet<Section> DataCollection, Section CurrentItem, string MultiBox)
        {
            _Logger.WriteLog("INFO");
            bool result = false;

            foreach (Section item in DataCollection)
            {
                if (item.Equals(CurrentItem))
                {
                    item.Line = MultiBox;
                    result = true;
                }
            }

            _Logger.WriteLog($"Trying to change line, result: {result}");
            return result;
        }
        #endregion

        #region Поиск

        /// <summary>
        /// Поиск текста в названиях диагнозов
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="MultiBox">Искомый текст</param>
        /// <returns>Список найденных диагнозов</returns>
        public ObservableCollection<Section> SearchDiagnosis(HashSet<Section> DataCollection, string MultiBox)
        {
            _Logger.WriteLog("INFO");
            bool result = false;
            var DiagnosisList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (item.Diagnosis.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
                {
                    DiagnosisList.Add(item);
                    result = true;
                }
            }

            _Logger.WriteLog($"Search result: {result}");
            return DiagnosisList;
        }

        /// <summary>
        /// Поиск текста в названиях разделов
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="MultiBox">Искомый текст</param>
        /// <returns>Список найденных разделов</returns>
        public ObservableCollection<Section> SearchBlocks(HashSet<Section> DataCollection, string MultiBox)
        {
            _Logger.WriteLog("INFO");
            bool result = false;
            var BlocksList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (item.Block.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase) &&
                    !BlocksList.Any(t => t.Diagnosis.Equals(item.Diagnosis) && t.Block.Equals(item.Block)))
                {
                    BlocksList.Add(item);
                    result = true;
                }
            }

            _Logger.WriteLog($"Search result: {result}");
            return BlocksList;
        }

        /// <summary>
        /// Поиск текста в строках
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="MultiBox">Искомый текст</param>
        /// <returns>Список найденных строк</returns>
        public ObservableCollection<Section> SearchLines(HashSet<Section> DataCollection, string MultiBox)
        {
            _Logger.WriteLog("INFO");
            bool result = false;

            var LinesList = new ObservableCollection<Section>(DataCollection
                .Where(t => t.Line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
                .Select(t => t));

            result = LinesList.Count > 0;
            _Logger.WriteLog($"Search result: {result}");
            return LinesList;
        }
        #endregion

        public string RandomDiary(HashSet<Section> DataCollection, Section CurrentItem)
        {
            _Logger.WriteLog("INFO");

            string result = "";
            Random rnd = new Random();

            if (CurrentDiagnosis != null)
            {
                foreach (Diagnosis diagnosis in DataCollection)
                {
                    if (diagnosis.Code.Equals(CurrentDiagnosis))
                    {
                        foreach (Block block in diagnosis.Blocks)
                        {
                            if (block.Lines.Count != 0)
                                result += block.Lines.ElementAt(rnd.Next(block.Lines.Count)) + " ";
                        }
                    }
                }
            }

            _Logger.WriteLog("RandomDiary created succesfully");
            return result;
        }

        /// <summary>Метод для добавления нового элемента</summary>
        /// <param name="FocusedDataGrid">Датагрида, в который добавляется элемент</param>
        /// <param name="MultiBox">Элемент, который нужно добавить</param>
        public void AddELement(string FocusedDataGrid, string MultiBox)
        {
            _Logger.WriteLog("INFO");

            if (CheckForDoubles(FocusedDataGrid, MultiBox)) return;

            if (FocusedDataGrid.Equals("Diagnosis"))
            {
                DataCollection.Add(new Diagnosis { Code = MultiBox, Blocks = new HashSet<Block>() });
                DiagnosisFromDataToView();
                return;
            }
            else
            {
                foreach (Diagnosis diagnosis in DataCollection)
                {
                    if (FocusedDataGrid.Equals("Blocks"))
                    {
                        if (diagnosis.Code.Equals(CurrentDiagnosis))
                        {
                            diagnosis.Blocks.Add(new Block { Name = MultiBox, Lines = new HashSet<string>() });
                            BlocksFromDataToView();
                            _Logger.WriteLog("DONE");
                            return;
                        }
                    }
                    else
                    {
                        foreach (Block block in diagnosis.Blocks)
                        {
                            if (block.Name.Equals(CurrentBlock))
                            {
                                block.Lines.Add(MultiBox);
                                LinesFromDataToView();
                                _Logger.WriteLog("DONE");
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
