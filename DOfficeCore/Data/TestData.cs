using System.Collections.ObjectModel;
using DOfficeCore.Models;

namespace DOfficeCore.Data
{
    class TestData
    {
        public static ObservableCollection<Section> GetCollection()
        {
            var TestLines = new ObservableCollection<Section>();

            for (int a = 1; a <= 5; a++)
            {
                for (int b = 1; b <= 5; b++)
                {
                    for (int c = 1; c <= 5; c++)
                    {
                        TestLines.Add(new Section()
                        {
                            Diagnosis = $"Diagnosis {a}",
                            Block = $"Block {b} in diagnosis {a}",
                            Line = $"Line {c} in block {b} in diagnosis {a}"
                        });
                    }
                }
            }
            return TestLines;
        }
    }
}
