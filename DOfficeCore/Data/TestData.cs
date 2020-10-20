using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DOfficeCore.Models;

namespace DOfficeCore.Data
{
    class TestData
    {
        private static HashSet<string> blockList = Enumerable.Range(1, 5)
            .Select(i => $"Line {i}")
            .ToHashSet();

        public static HashSet<string> BlockList { get => blockList; set => blockList = value; }


        private static HashSet<Block> blocks = Enumerable.Range(1, 5)
            .Select(i => new Block
            {
                Name = $"Block {i}",
                Lines = new HashSet<string>(BlockList),
            }).ToHashSet();

        public static HashSet<Block> Blocks { get => blocks; set => blocks = value; }


        private static HashSet<Diagnosis> diag = Enumerable.Range(1, 5)
            .Select(i => new Diagnosis
            {
                Code = $"Diagnosis {i}",
                Blocks = new HashSet<Block>(Blocks),
            }).ToHashSet();

        public static HashSet<Diagnosis> Diag { get => diag; set => diag = value; }

        public static ObservableCollection<Diagnosis> Diagnoses { get; set; } = new ObservableCollection<Diagnosis>(Diag);
    }
}
