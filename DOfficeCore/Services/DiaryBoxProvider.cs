using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

using DOfficeCore.Services.Interfaces;
using DOfficeCore.Models;

namespace DOfficeCore.Services
{
    ///<inheritdoc cref="IDiaryBoxProvider"/>
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        ///<inheritdoc/>
        public string LineToDiaryBox(string DiaryBox, string Line)
        {
            if (DiaryBox == null) DiaryBox = "";
            if (string.IsNullOrEmpty(Line)) return DiaryBox;
            if (DiaryBox.Contains(Line + " ")) return DiaryBox.Remove(DiaryBox.IndexOf(Line), Line.Length + 1);
            else return DiaryBox + Line + " ";
        }

        ///<inheritdoc/>
        public (string Diary, ObservableCollection<string> Lines) RandomDiaryWithNewModel(
            IEnumerable<Block> Blocks, 
            int ChanceMidofier = 20)
        {
            string Diary = "";
            var rnd = new Random();

            var Lines = new ObservableCollection<string>();
            foreach (Block block in Blocks)
            {
                var LineList = block.Lines;
                if (LineList.Count > 0 && rnd.Next(100) <= LineList.Count * ChanceMidofier)
                {
                    var line = LineList[rnd.Next(LineList.Count)];
                    Lines.Add(line);
                    Diary += line + " ";
                }
            }

            return (Diary, Lines);
        }
    }
}