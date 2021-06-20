﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    interface IDiaryBoxProvider
    {
        string LineToDiaryBox(string DiaryBox, string Line);
        (string, ObservableCollection<Section>) RandomDiary(List<Section> DataCollection, Section CurrentSection);
        (string, ObservableCollection<string>) RandomDiaryWithNewModel(IEnumerable<Block> Blocks);
    }
}
