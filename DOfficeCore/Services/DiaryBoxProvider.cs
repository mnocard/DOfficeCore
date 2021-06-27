using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

using DOfficeCore.Services.Interfaces;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        private const int _ChanceMidofier = 20;

        //private readonly IViewCollectionProvider _ViewCollectionProvider;
        //public DiaryBoxProvider(IViewCollectionProvider ViewCollectionProvider)
        //{
        //    _ViewCollectionProvider = ViewCollectionProvider;
        //}

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