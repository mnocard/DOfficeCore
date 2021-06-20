using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

using DOfficeCore.Services.Interfaces;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        private readonly IViewCollectionProvider _ViewCollectionProvider;
        private const int _ChanceMidofier = 20;

        public DiaryBoxProvider(IViewCollectionProvider ViewCollectionProvider)
        {
            _ViewCollectionProvider = ViewCollectionProvider;
        }

        /// <summary>
        /// Добавлением строки в дневник с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Дневник</param>
        /// <param name="Line">Добавляемая строка</param>
        /// <returns>Измененный дневник</returns>
        public string LineToDiaryBox(string DiaryBox, string Line)
        {
            if (DiaryBox == null) DiaryBox = "";
            if (string.IsNullOrEmpty(Line)) return DiaryBox;
            if (DiaryBox.Contains(Line + " ")) return DiaryBox.Remove(DiaryBox.IndexOf(Line), Line.Length + 1);
            else return DiaryBox + Line + " ";
        }


        /// <summary>
        /// Создание дневника, по одной случайной строке из каждого раздела определенного диагноза 
        /// с шансом попадания строки в дневник зависимости от количества строк в дневнике
        /// </summary>
        /// <param name="DataCollection">Коллекция элементов базы данных</param>
        /// <param name="CurrentSection">Выбранная секция базы данных</param>
        /// <returns>Случайный дневник</returns>
        public (string, ObservableCollection<Section>) RandomDiary(List<Section> DataCollection, Section CurrentSection)
        {
            string result = "";
            var rnd = new Random();

            var BlockList = _ViewCollectionProvider.BlocksFromDataToView(DataCollection, CurrentSection);
            var linesOfDiary = new ObservableCollection<Section>();
            foreach (Section block in BlockList)
            {
                var LineList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, block);
                if (LineList.Count > 0 && rnd.Next(100) <= LineList.Count * _ChanceMidofier)
                {
                    var section = LineList[rnd.Next(LineList.Count)];
                    linesOfDiary.Add(section);
                    result += section.Line + " ";
                }
            }

            return (result, linesOfDiary);
        }

        public (string, ObservableCollection<string>) RandomDiaryWithNewModel(IEnumerable<Block> Blocks)
        {
            string result = "";
            var rnd = new Random();

            var linesOfDiary = new ObservableCollection<string>();
            foreach (Block block in Blocks)
            {
                var LineList = block.Lines;
                if (LineList.Count > 0 && rnd.Next(100) <= LineList.Count * _ChanceMidofier)
                {
                    var line = LineList[rnd.Next(LineList.Count)];
                    linesOfDiary.Add(line);
                    result += line + " ";
                }
            }

            return (result, linesOfDiary);
        }
    }
}