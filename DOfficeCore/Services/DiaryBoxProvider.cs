using DOfficeCore.Logger;
using DOfficeCore.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        private readonly ILogger _Logger;

        public DiaryBoxProvider(ILogger Logger) => _Logger = Logger;

        public string DocToDiary(string DiaryBox, string position, string doctor)
        {
            _Logger.WriteLog("INFO");

            if (DiaryBox == null) DiaryBox = "";
            if (DiaryBox.Contains(position) && DiaryBox.Contains(doctor))
            {
                var result =  DiaryBox.Remove(DiaryBox.IndexOf(position) - 1);
                _Logger.WriteLog("DONE");
                return result;

            }
            else
            {
                var result = DiaryBox +"\n" + position + "\t\t\t" + doctor;
                _Logger.WriteLog("DONE");
                return result;
            }
        }

        public string LineToDiaryBox(string DiaryBox, string Line)
        {
            _Logger.WriteLog("INFO");

            if (DiaryBox == null) DiaryBox = "";
            if (DiaryBox.Contains(Line + " "))
            {
                var result = DiaryBox.Remove(DiaryBox.IndexOf(Line), Line.Length + 1);
                _Logger.WriteLog("DONE");
                return result;
            }
            else
            {
                var result = DiaryBox + Line + " ";
                _Logger.WriteLog("DONE");
                return result;
            }
        }

        public string DateToDiaryBox(string DiaryBox, DateTime ChoosenDate)
        {
            _Logger.WriteLog("INFO");

            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            string firstLine = reader.ReadLine();
            if (DateTime.TryParse(firstLine, out _))
            {
                var result = DiaryBox.Replace(firstLine + "\n", "");
                _Logger.WriteLog("DONE");
                return result;
            }
            else
            {
                var result = DiaryBox.Insert(0, ChoosenDate.ToString("dd.MM.yyyy") + "\n");
                _Logger.WriteLog("DONE");
                return result;
            }
        }

        public string TimeToDiaryBox(string DiaryBox, DateTime ChoosenTime)
        {
            _Logger.WriteLog("INFO");

            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            string firstLine = reader.ReadLine();

            if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 10)
            {
                var result = DiaryBox.Replace(firstLine, firstLine + " " + ChoosenTime.ToString("HH:mm"));
                _Logger.WriteLog("DONE");
                return result;
            }
            else if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 16)
            {
                var result = DiaryBox.Replace(firstLine, ChoosenTime.ToString("dd.MM.yyyy"));
                _Logger.WriteLog("DONE");
                return result;
            }
            _Logger.WriteLog("DONE");
            return DiaryBox;
        }
    }
}
