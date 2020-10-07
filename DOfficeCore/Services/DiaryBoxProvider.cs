using DOfficeCore.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DOfficeCore.Services
{
    class DiaryBoxProvider : IDiaryBoxProvider
    {
        public string DocToDiary(string DiaryBox, string position, string doctor)
        {
            if (DiaryBox == null) DiaryBox = "";
            if (DiaryBox.Contains(position) && DiaryBox.Contains(doctor))
            {
                return DiaryBox.Remove(DiaryBox.IndexOf(position) - 1);
            }
            else
            {
                return DiaryBox +"\n" + position + "\t\t\t" + doctor;
            }
        }

        public string LineToDiaryBox(string DiaryBox, string Line)
        {
            if (DiaryBox == null) DiaryBox = "";
            if (DiaryBox.Contains(Line + " "))
            {
                return DiaryBox.Remove(DiaryBox.IndexOf(Line), Line.Length + 1);
            }
            else
            {
                return DiaryBox + Line + " ";
            }
        }

        public string DateToDiaryBox(string DiaryBox, DateTime ChoosenDate)
        {
            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            string firstLine = reader.ReadLine();
            if (DateTime.TryParse(firstLine, out _))
            {
                return DiaryBox.Replace(firstLine + "\n", "");
            }
            else
            {
                return DiaryBox.Insert(0, ChoosenDate.ToString("dd.MM.yyyy") + "\n");
            }
        }

        public string TimeToDiaryBox(string DiaryBox, DateTime ChoosenTime)
        {
            if (DiaryBox == null) DiaryBox = "";
            using var reader = new StringReader(DiaryBox);
            string firstLine = reader.ReadLine();

            if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 10)
            {
                return DiaryBox.Replace(firstLine, firstLine + " " + ChoosenTime.ToString("HH:mm"));
            }
            else if (DateTime.TryParse(firstLine, out _) && firstLine.Length == 16)
            {
                return DiaryBox.Replace(firstLine, ChoosenTime.ToString("dd.MM.yyyy"));
            }
            return DiaryBox;
        }
    }
}
