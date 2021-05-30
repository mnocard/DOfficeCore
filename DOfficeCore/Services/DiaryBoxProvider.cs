using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
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
    }
}
