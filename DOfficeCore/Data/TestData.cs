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
        public static List<string> blockList = Enumerable.Range(1, 20)
            .Select(i => $"Line {i}")
            .ToList();

        public static List<Block> blocks = Enumerable.Range(1, 10)
            .Select(i => new Block
            {
                Name = $"Block {i}",
                Lines = new ObservableCollection<string>(blockList),
            }).ToList();

        public static List<Diagnosis> diag = Enumerable.Range(1, 10)
            .Select(i => new Diagnosis
            {
                Code = $"Diagnosis {i}",
                Blocks = new ObservableCollection<Block>(blocks),
            }).ToList();

        public static ObservableCollection<Diagnosis> diagnoses { get; set; } = new ObservableCollection<Diagnosis>(diag);
        
    }
}
