using System;

namespace DOfficeCore.Services.Interfaces
{
    interface IDiaryBoxProvider
    {
        string LineToDiaryBox(string DiaryBox, string Line);
        string DateToDiaryBox(string DiaryBox, DateTime ChoosenDate);
        string TimeToDiaryBox(string DiaryBox, DateTime ChoosenTime);
    }
}
