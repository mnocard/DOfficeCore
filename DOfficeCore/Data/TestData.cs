using System;
using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;

namespace DOfficeCore.Data
{
    class TestData
    {
        public static List<Section> GetSectionCollection()
        {
            var TestLines = new List<Section>();

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
                            Line = $"Line {c} in block {b} in diagnosis {a}."
                        });
                    }
                }
            }
            return TestLines;
        }

        public static List<Sector> GetSectorCollection()
        {
            Random rnd = new Random();

            return Enumerable.Range(1, 10).Select(i => new Sector
            {
                Name = $"Sector {i}",
                Blocks = Enumerable.Range(1, rnd.Next(10)).Select(k => new Block
                {
                    Name = $"Block {k}",
                    Sector = $"Sector {i}",
                    Lines = Enumerable.Range(1, rnd.Next(10)).Select(l => $"Line {l}").ToList(),
                }).ToList(),
            }).ToList();
        }
    }
}
