using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DOfficeCore.Data
{
    class TestData
    {
        private static List<string> blockList = Enumerable.Range(1, 5)
            .Select(i => $"Line {i}")
            .ToList();

        public static List<string> BlockList { get => blockList; set => blockList = value; }


        private static List<Block> blocks = Enumerable.Range(1, 5)
            .Select(i => new Block
            {
                Name = $"Block {i}",
                Lines = new List<string>(BlockList),
            }).ToList();

        internal static List<Block> Blocks { get => blocks; set => blocks = value; }


        private static List<Diagnosis> diag = Enumerable.Range(1, 5)
            .Select(i => new Diagnosis
            {
                Code = $"Diagnosis {i}",
                Blocks = new List<Block>(Blocks),
            }).ToList();

        internal static List<Diagnosis> Diag { get => diag; set => diag = value; }

        public static ObservableCollection<Diagnosis> Diagnoses { get; set; } = new ObservableCollection<Diagnosis>(Diag);
    }
}
