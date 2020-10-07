using System;

namespace DOfficeCore.Services.Interfaces
{
    interface IDiaryBoxProvider
    {
        string DocToDiary(string DiaryBox, string position, string doctor);
        string LineToDiaryBox(string DiaryBox, string Line);
        string DateToDiaryBox(string DiaryBox, DateTime ChoosenDate);
        string TimeToDiaryBox(string DiaryBox, DateTime ChoosenTime);
    }
}
