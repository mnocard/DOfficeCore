using System;
using System.IO;
using DOfficeCore.Logger;
using DOfficeCore.Services.Interfaces;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        private readonly ILogger _Logger;

        public DiaryBoxProvider(ILogger Logger) => _Logger = Logger;

        /// <summary>
        /// Добавлением строки с именем и должностью врача в конец дневника с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Оригинальный дневник</param>
        /// <param name="position">Должность врача</param>
        /// <param name="doctor">ФИО врача</param>
        /// <returns>Измененный дневник</returns>
        public string DocToDiary(string DiaryBox, string position, string doctor)
        {
            _Logger.WriteLog($"Trying to add {position} and {doctor} the diary");

            if (DiaryBox == null) DiaryBox = "";
            if (DiaryBox.Contains(position) && DiaryBox.Contains(doctor))
            {
                var result =  DiaryBox.Remove(DiaryBox.IndexOf(position) - 1);
                _Logger.WriteLog("Position and name of choosen doctor was added earlier. Now they are removed.");
                return result;

            }
            else
            {
                var result = DiaryBox +"\n" + position + "\t\t\t" + doctor;
                _Logger.WriteLog("Position and name of choosen doctor was added succesfully.");
                return result;
            }
        }

        /// <summary>
        /// Добавлением строки в дневник с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Дневник</param>
        /// <param name="Line">Добавляемая строка</param>
        /// <returns>Измененный дневник</returns>
        public string LineToDiaryBox(string DiaryBox, string Line)
        {
            _Logger.WriteLog("Trying to add line to the diary");

            if (DiaryBox == null) DiaryBox = "";
            if (DiaryBox.Contains(Line + " "))
            {
                var result = DiaryBox.Remove(DiaryBox.IndexOf(Line), Line.Length + 1);
                _Logger.WriteLog("Choosen line was added earlier. Now it's removed.");
                return result;
            }
            else
            {
                var result = DiaryBox + Line + " ";
                _Logger.WriteLog("Choosen line was added succesfully.");
                return result;
            }
        }
        /// <summary>
        /// Добавление даты в начало дневника с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Оригинальный дневник</param>
        /// <param name="ChoosenDate">Выбранная дата</param>
        /// <returns>Измененный дневник</returns>
        public string DateToDiaryBox(string DiaryBox, DateTime ChoosenDate)
        {
            _Logger.WriteLog($"Trying to add date {ChoosenDate:dd.MM.yyyy} to the diary");

            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            string firstLine = reader.ReadLine();
            if (DateTime.TryParse(firstLine, out _))
            {
                var result = DiaryBox.Replace(firstLine + "\n", "");
                _Logger.WriteLog("Choosen date was added earlier. Now it's removed.");
                return result;
            }
            else
            {
                var result = DiaryBox.Insert(0, ChoosenDate.ToString("dd.MM.yyyy") + "\n");
                _Logger.WriteLog("Choosen date was added succesfully.");
                return result;
            }
        }
        /// <summary>
        /// Добавление времени в дневник с возможностью удаления вместо повторного добавления
        /// </summary>
        /// <param name="DiaryBox">Оригинальный дневник</param>
        /// <param name="ChoosenTime">Добавляемое время</param>
        /// <returns>Измененный дневник</returns>
        public string TimeToDiaryBox(string DiaryBox, DateTime ChoosenTime)
        {
            _Logger.WriteLog($"Trying to add time {ChoosenTime:HH: mm}  to the diary");

            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            string firstLine = reader.ReadLine();

            if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 10)
            {
                var result = DiaryBox.Replace(firstLine, firstLine + " " + ChoosenTime.ToString("HH:mm"));
                _Logger.WriteLog("Choosen time was added earlier. Now it's removed.");
                return result;
            }
            else if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 16)
            {
                var result = DiaryBox.Replace(firstLine, ChoosenTime.ToString("dd.MM.yyyy"));
                _Logger.WriteLog("Choosen time was added succesfully.");
                return result;
            }
            return DiaryBox;
        }
    }
}
