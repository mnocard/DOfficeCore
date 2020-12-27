using System;
using System.IO;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        /// <summary>
        /// Добавлением строки с именем и должностью врача в конец дневника с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Оригинальный дневник</param>
        /// <param name="position">Должность врача</param>
        /// <param name="doctor">ФИО врача</param>
        /// <returns>Измененный дневник</returns>
        public string DocToDiary(string DiaryBox, string position, string doctor)
        {
            if (DiaryBox == null) DiaryBox = "";
            if (string.IsNullOrEmpty(position) || string.IsNullOrEmpty(doctor)) return DiaryBox;
            if (DiaryBox.Contains(position) && DiaryBox.Contains(doctor)) return DiaryBox.Remove(DiaryBox.IndexOf(position) - 1);
            else return DiaryBox + "\n" + position + "\t\t\t" + doctor;
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
        /// Добавление даты в начало дневника с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Оригинальный дневник</param>
        /// <param name="ChoosenDate">Выбранная дата</param>
        /// <returns>Измененный дневник</returns>
        public string DateToDiaryBox(string DiaryBox, DateTime ChoosenDate)
        {
            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            var firstLine = reader.ReadLine();
            if (DateTime.TryParse(firstLine, out _)) return DiaryBox.Replace(firstLine + "\n", "");
            else return DiaryBox.Insert(0, ChoosenDate.ToString("dd.MM.yyyy") + "\n");
        }
        
        /// <summary>
        /// Добавление времени в дневник с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Оригинальный дневник</param>
        /// <param name="ChoosenTime">Добавляемое время</param>
        /// <returns>Измененный дневник</returns>
        public string TimeToDiaryBox(string DiaryBox, DateTime ChoosenTime)
        {
            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            var firstLine = reader.ReadLine();

            if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 10) return DiaryBox.Replace(firstLine, firstLine + " " + ChoosenTime.ToString("HH:mm"));
            else if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 16) return DiaryBox.Replace(firstLine, ChoosenTime.ToString("dd.MM.yyyy"));
            else return DiaryBox.Insert(0, ChoosenTime.ToString("dd.MM.yyyy HH:mm") + "\n");
        }
    }
}
