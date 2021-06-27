using System;
using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;

namespace DOfficeCore.Data
{
    class TestData
    {
        public static List<Sector> GetSectorCollection()
        {
            Random rnd = new();

            return Enumerable.Range(1, 10).Select(i => new Sector
            {
                Name = $"Сектор {i}",
                Blocks = Enumerable.Range(1, rnd.Next(3, 10)).Select(k => new Block
                {
                    Name = $"Блок {k}",
                    Sector = $"Сектор {i}",
                    Lines = Enumerable.Range(1, rnd.Next(10)).Select(l => $"Строка {l} в блоке {k} в секторе {i}").ToList(),
                }).ToList(),
            }).ToList();
        }
    }
}
