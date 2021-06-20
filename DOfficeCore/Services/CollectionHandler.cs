using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    public class CollectionHandler : ICollectionHandler
    {
        #region Удаление элементов

        /// <summary>
        /// Удаление всех элементов диагноза
        /// </summary>
        /// <param name="DataCollection">Коллекция элемента</param>
        /// <param name="CurrentSection">Секция, из которого берется диагноз</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveDiagnosis(List<Section> DataCollection, Section CurrentSection) =>
              DataCollection.RemoveAll(t => t.Diagnosis.Equals(CurrentSection.Diagnosis)) > 0;

        /// <summary>
        /// Удаление всех элементов раздела
        /// </summary>
        /// <param name="DataCollection">Коллекция из которой происходит удаление</param>
        /// <param name="CurrentSection">Секция, из которой берутся данные о диагнозе и разделе</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveBlock(List<Section> DataCollection, Section CurrentSection) =>
            DataCollection.RemoveAll(t => t.Diagnosis.Equals(CurrentSection.Diagnosis) && t.Block.Equals(CurrentSection.Block)) > 0;

        /// <summary>
        /// Удаление строки
        /// </summary>
        /// <param name="DataCollection">Коллекция из которой происходит удаление</param>
        /// <param name="CurrentSection">Секция, содержащая строку</param>
        /// <returns>True если удаление прошло успешно</returns>
        public bool RemoveLine(List<Section> DataCollection, Section CurrentSection) => DataCollection.Remove(CurrentSection);
        #endregion
        #region Добавление
        /// <summary>
        /// Добавление диагноза в коллекцию данных
        /// </summary>
        /// <param name="DataCollection">Коллекция, в которую происходит добавление</param>
        /// <param name="MultiBox">Диагноз, который необходимо добавить</param>
        /// <returns>True если успешно добавлено</returns>
        public Section AddDiagnosis(List<Section> DataCollection, string MultiBox)
        {
            if (string.IsNullOrWhiteSpace(MultiBox) ||
                DataCollection.Any(section => section.Diagnosis.Equals(MultiBox)))
                return null;

            var newSection = new Section() { Diagnosis = MultiBox };
            DataCollection.Add(newSection);
            return newSection;
        }

        /// <summary>
        /// Добавление раздела в коллекцию данных
        /// </summary>
        /// <param name="DataCollection">Коллекция, в которую происходит добавление</param>
        /// <param name="CurrentSection">Секция, предназначенная для получения диагноза, в которой будет находится раздел</param>
        /// <param name="MultiBox">Раздел, который необходимо добавить</param>
        /// <returns>True если успешно добавлено</returns>
        public Section AddBlock(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            if (CurrentSection.Diagnosis is null ||
                string.IsNullOrWhiteSpace(MultiBox) ||
                DataCollection.Any(section =>
                    section.Diagnosis.Equals(CurrentSection.Diagnosis) &&
                    section.Block is not null &&
                    section.Block.Equals(MultiBox)))
                return null;

            if (DataCollection.FirstOrDefault(section =>
                    section.Diagnosis.Equals(CurrentSection.Diagnosis) &&
                    section.Block is null)
                is Section section)
            {
                section.Block = MultiBox;
                return section;
            }

            var newSection = new Section() { Diagnosis = CurrentSection.Diagnosis, Block = MultiBox };
            DataCollection.Add(newSection);
            return newSection;
        }

        /// <summary>
        /// Добавление строки в коллекцию данных
        /// </summary>
        /// <param name="DataCollection">Коллекция, в которую происходит добавление</param>
        /// <param name="CurrentSection">Секция, предназначенная для получения диагноза и раздела, в которой будет находится строка</param>
        /// <param name="MultiBox">Строка, которую необходимо добавить</param>
        /// <returns>True если успешно добавлено</returns>
        public Section AddLine(List<Section> DataCollection, Section CurrentSection, string MultiBox)
        {
            if (CurrentSection.Block is null ||
                string.IsNullOrWhiteSpace(MultiBox) ||
                DataCollection.Any(section =>
                    section.Diagnosis.Equals(CurrentSection.Diagnosis) &&
                    section.Block.Equals(CurrentSection.Block) &&
                    section.Line is not null &&
                    section.Line.Equals(MultiBox)))
                return null;

            if (DataCollection.FirstOrDefault(section =>
                    section.Diagnosis.Equals(CurrentSection.Diagnosis) &&
                    section.Block.Equals(CurrentSection.Block) &&
                    section.Line is null)
                is Section section)
            {
                section.Line = MultiBox;
                return section;
            }

            var newSection = new Section
            {
                Diagnosis = CurrentSection.Diagnosis,
                Block = CurrentSection.Block,
                Line = MultiBox
            };
            DataCollection.Add(newSection);
            return newSection;
        }
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
                if (item.Diagnosis.Equals(CurrentSection.Diagnosis))
                {
                    item.Diagnosis = MultiBox;
                    result = true;
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
    }
}
