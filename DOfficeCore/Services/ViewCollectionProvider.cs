using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {
        #region Получение списков

        /// <summary>
        /// Получение списка диагнозов для модели представления
        /// </summary>
        /// <param name="DataCollection">Коллекция элементов базы данных</param>
        /// <returns>Список диагнозов</returns>
        public ObservableCollection<Section> DiagnosisFromDataToView(IEnumerable<Section> DataCollection)
        {
            if (DataCollection == null)
            {
                DataCollection = new List<Section>();
                return new ObservableCollection<Section>();
            }

            var DiagnosisList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (DiagnosisList.Count == 0) DiagnosisList.Add(item);
                else if (!DiagnosisList.Any(t => t.Diagnosis.Equals(item.Diagnosis))) DiagnosisList.Add(item);
            }

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
            if (CurrentSection == null || CurrentSection.Block == null) return new ObservableCollection<Section>();
            
            var BlockList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (BlockList.Count == 0 && item.Diagnosis.Equals(CurrentSection.Diagnosis)) BlockList.Add(item);
                else if (item.Diagnosis.Equals(CurrentSection.Diagnosis) && !BlockList.Any(t => t.Block.Equals(item.Block))) BlockList.Add(item);
            }

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
            if (CurrentSection == null || CurrentSection.Block == null || CurrentSection.Line == null) return new ObservableCollection<Section>();

            var LineList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (item.Block != null && item.Block.Equals(CurrentSection.Block) && item.Diagnosis.Equals(CurrentSection.Diagnosis)) LineList.Add(item);
            }

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
        public bool RemoveDiagnosis(List<Section> DataCollection, Section CurrentSection)
        {
            var result = DataCollection.RemoveAll(t => t.Diagnosis.Equals(CurrentSection.Diagnosis));

            if (result > 0) return true;
            else return false;
        }

        /// <summary>
        /// Удаление всех элементов раздела
        /// </summary>
        /// <param name="DataCollection">Коллекция из которой происходит удаление</param>
        /// <param name="CurrentSection">Секция, из которой берутся данные о диагнозе и разделе</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveBlock(List<Section> DataCollection, Section CurrentSection)
        {
            var result = DataCollection.RemoveAll(t => t.Diagnosis.Equals(CurrentSection.Diagnosis) && t.Block.Equals(CurrentSection.Block));

            if (result > 0) return true;
            else return false;
        }

        /// <summary>
        /// Удаление строки
        /// </summary>
        /// <param name="DataCollection">Коллекция из которой происходит удаление</param>
        /// <param name="CurrentSection">Секция, содержащая строку</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveLine(List<Section> DataCollection, Section CurrentSection) => DataCollection.Remove(CurrentSection);
        #endregion

        #region Переименовывание

        /// <summary>
        /// Переименовывание диагноза
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="CurrentSection">Секция, из которой берется старое название диагноз</param>
        /// <param name="MultiBox">Новое название диагноза</param>
        /// <returns>True если успешно переименовано</returns>
        public bool EditDiagnosis(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            bool result = false;

            foreach (Section item in DataCollection)
            {
                if (item.Diagnosis.Equals(CurrentSection.Diagnosis))
                {
                    item.Diagnosis = MultiBox;
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Переименовывание раздела
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="CurrentSection">Секция, из которой берется старое название блока и диагноза, в котором блок находится</param>
        /// <param name="MultiBox">Новое название блока</param>
        /// <returns>True если успешно переименовано</returns>
        public bool EditBlock(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            bool result = false;

            foreach (Section item in DataCollection)
            {
                if (item.Diagnosis.Equals(CurrentSection.Diagnosis) && item.Block.Equals(CurrentSection.Block))
                {
                    item.Block = MultiBox;
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Переименовывание строки
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="CurrentSection">Секция, из которой берется название блока и диагноза, в которой находится строка</param>
        /// <param name="MultiBox">Новая строка</param>
        /// <returns>True если успешно переименовано</returns>
        public bool EditLine(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            bool result = false;

            foreach (Section item in DataCollection)
            {
                if (item.Equals(CurrentSection))
                {
                    item.Line = MultiBox;
                    result = true;
                }
            }

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
        public ObservableCollection<Section> SearchDiagnosis(List<Section> DataCollection, string MultiBox)
        {
            var DiagnosisList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (item.Diagnosis.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase) &&
                    !DiagnosisList.Any(t => t.Diagnosis.Equals(item.Diagnosis)))
                {
                    DiagnosisList.Add(item);
                }
            }

            return DiagnosisList;
        }

        /// <summary>
        /// Поиск текста в названиях разделов
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="MultiBox">Искомый текст</param>
        /// <returns>Список найденных разделов</returns>
        public ObservableCollection<Section> SearchBlocks(List<Section> DataCollection, string MultiBox)
        {
            var BlocksList = new ObservableCollection<Section>();

            foreach (Section item in DataCollection)
            {
                if (item.Block.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase) &&
                    !BlocksList.Any(t => t.Diagnosis.Equals(item.Diagnosis) && t.Block.Equals(item.Block)))
                {
                    BlocksList.Add(item);
                }
            }

            return BlocksList;
        }

        /// <summary>
        /// Поиск текста в строках
        /// </summary>
        /// <param name="DataCollection">Коллекция данных</param>
        /// <param name="MultiBox">Искомый текст</param>
        /// <returns>Список найденных строк</returns>
        public ObservableCollection<Section> SearchLines(List<Section> DataCollection, string MultiBox)
        {
            return new ObservableCollection<Section>(DataCollection
                .Where(t => t.Line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
                .Select(t => t));
        }
        #endregion

        #region Добавление
        /// <summary>
        /// Добавление диагноза в коллекцию данных
        /// </summary>
        /// <param name="DataCollection">Коллекция, в которую происходит добавление</param>
        /// <param name="MultiBox">Диагноз, который необходимо добавить</param>
        /// <returns>True если успешно добавлено</returns>
        public bool AddDiagnosis(List<Section> DataCollection, string MultiBox)
        {
            if (MultiBox != null && MultiBox != "")
            {
                if (DataCollection.Count == 0)
                {
                    DataCollection.Add(new Section() { Diagnosis = MultiBox });
                    return true;
                }
                foreach (var _ in DataCollection.Where(item => item.Diagnosis.Equals(MultiBox)).Select(item => new { })) return false;

                DataCollection.Add(new Section() { Diagnosis = MultiBox });
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Добавление раздела в коллекцию данных
        /// </summary>
        /// <param name="DataCollection">Коллекция, в которую происходит добавление</param>
        /// <param name="CurrentSection">Секция, предназначенная для получения диагноза, в которой будет находится раздел</param>
        /// <param name="MultiBox">Раздел, который необходимо добавить</param>
        /// <returns>True если успешно добавлено</returns>
        public bool AddBlock(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            if (CurrentSection.Diagnosis != null && MultiBox != null && MultiBox != "")
            {
                foreach (Section item in DataCollection)
                {
                    if (item.Diagnosis.Equals(CurrentSection.Diagnosis))
                    {
                        if (item.Block == null)
                        {
                            item.Block = MultiBox;
                            return true;
                        }
                        else if (item.Block.Equals(MultiBox)) return false;
                    }
                }

                DataCollection.Add(new Section() { Diagnosis = CurrentSection.Diagnosis, Block = MultiBox });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Добавление строки в коллекцию данных
        /// </summary>
        /// <param name="DataCollection">Коллекция, в которую происходит добавление</param>
        /// <param name="CurrentSection">Секция, предназначенная для получения диагноза и раздела, в которой будет находится строка</param>
        /// <param name="MultiBox">Строка, которую необходимо добавить</param>
        /// <returns>True если успешно добавлено</returns>
        public bool AddLine(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            if (CurrentSection.Diagnosis != null && CurrentSection.Block != null)
            {
                foreach (Section item in DataCollection)
                {
                    if(item.Diagnosis.Equals(CurrentSection.Diagnosis) && item.Block.Equals(CurrentSection.Block) && item.Line == null)
                    {
                        item.Line = MultiBox;
                        return true;
                    }
                }
            }

            DataCollection.Add(new Section() { Diagnosis = CurrentSection.Diagnosis, Block = CurrentSection.Block, Line = MultiBox });
            return true;
        }
        #endregion

        #region Создание случайного дневника

        /// <summary>
        /// Создание дневника, по одной случайной строке из каждого раздела определенного диагноза
        /// </summary>
        /// <param name="DataCollection">Коллекция элементов базы данных</param>
        /// <param name="CurrentSection">Выбранная секция базы данных</param>
        /// <returns>Случайный дневник</returns>
        public string RandomDiary(List<Section> DataCollection, Section CurrentSection)
        {
            string result = "";
            var rnd = new Random();

            var BlockList = BlocksFromDataToView(DataCollection, CurrentSection);

            foreach (Section item in BlockList)
            {
                var LineList = LinesFromDataToView(DataCollection, item);
                if (LineList.Count > 0) result += LineList[rnd.Next(LineList.Count)].Line + " ";
            }

            return result;
        }
        #endregion

    }
}
