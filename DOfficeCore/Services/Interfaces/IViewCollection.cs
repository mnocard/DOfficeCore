using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DOfficeCore.Models
{
    interface IViewCollection
    {
        List<Diagnosis> DataCollection { get; set; }

        string CurrentBlock { get; set; }
        string CurrentDiagnosis { get; set; }
        string CurrentLine { get; set; }

        ObservableCollection<string> BlocksNames { get; set; }
        ObservableCollection<string> DiagnosisCode { get; set; }
        ObservableCollection<string> LinesNames { get; set; }
    }
}