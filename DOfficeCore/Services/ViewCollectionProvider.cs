using DOfficeCore.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    [Obsolete("Класс устарел. Используй NewViewCollectionProvider", true)]
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
                if (!DiagnosisList.Any(t => t.Diagnosis.Equals(item.Diagnosis)))
                    DiagnosisList.Add(item);

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
                if (item.Diagnosis.Equals(CurrentSection.Diagnosis))
                    if (!BlockList.Any(t => t.Block.Equals(item.Block)))
                        BlockList.Add(item);

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
                if (item.Block != null && item.Block.Equals(CurrentSection.Block) && item.Diagnosis.Equals(CurrentSection.Diagnosis))
                    LineList.Add(item);
            }

            return LineList;
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
    }
}
