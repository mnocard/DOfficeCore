using System;
using System.Collections.Generic;
using System.Linq;

using DOfficeCore.Models;

namespace DOfficeCore.Data
{
    /// <summary>
    /// Класс для создания тестовых данных
    /// </summary>
    static class TestData
    {
        /// <summary>
        /// Создание тестовых данных для приложения
        /// </summary>
        /// <returns>Список созданных секторов</returns>
        public static List<Sector> GetSectorCollection()
        {
            Random rnd = new();

            return Enumerable.Range(1, 7).Select(i => new Sector
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
