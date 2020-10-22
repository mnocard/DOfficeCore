using DOfficeCore.Logger;
using DOfficeCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {
        public ViewCollectionProvider(ILogger Logger)
        {
            _Logger = Logger;
        }

        #region Сервисы
        private readonly ILogger _Logger;
        #endregion

        //#region Методы
        
        ///// <summary>Метод создание случайного дневника </summary>
        ///// <returns>Случайный дневник</returns>
        //public string RandomDiary(string CurrentDiagnosis, HashSet<Diagnosis> DataCollection)
        //{
        //    _Logger.WriteLog("INFO");

        //    string result = "";
        //    Random rnd = new Random();
        //    if (CurrentDiagnosis != null)
        //    {
        //        foreach (Diagnosis diagnosis in DataCollection)
        //        {
        //            if(diagnosis.Code.Equals(CurrentDiagnosis))
        //            {
        //                foreach (Block block in diagnosis.Blocks)
        //                {
        //                    if (block.Lines.Count != 0)
        //                        result += block.Lines.ElementAt(rnd.Next(block.Lines.Count)) + " ";
        //                }
        //            }
        //        }
        //    }

        //    _Logger.WriteLog("RandomDiary created succesfully");
        //    return result;
        //}

        ///// <summary>Метод для отображения списка диагнозов в таблице</summary>
        //public ObservableCollection<string> DiagnosisFromDataToView(HashSet<Diagnosis> DataCollection)
        //{
        //    _Logger.WriteLog("INFO");

        //    if (DataCollection == null)
        //    {
        //        _Logger.WriteLog("DataCollection is null");
        //        DataCollection = new HashSet<Diagnosis>();
        //    }

        //    var  DiagnosisCode = new ObservableCollection<string>(DataCollection.
        //        Select(t => t.Code));
        //    _Logger.WriteLog("DiagnosisCode returned succesfully");
        //    return DiagnosisCode;

        //}

        ///// <summary>Метод для отображения списка блоков в таблице</summary>
        //public ObservableCollection<string> BlocksFromDataToView(string CurrentDiagnosis, HashSet<Diagnosis> DataCollection)
        //{
        //    _Logger.WriteLog("INFO");

        //    if (CurrentDiagnosis == null) return new ObservableCollection<string>();
        //    var BlocksNames = new ObservableCollection<string>();

        //    foreach (Diagnosis diagnosis in DataCollection)
        //    {
        //        if(diagnosis.Code.Equals(CurrentDiagnosis))
        //        {
        //            foreach (Block block in diagnosis.Blocks)
        //            {
        //                BlocksNames.Add(block.Name);
        //            }
        //            _Logger.WriteLog("BlocksNames returned succesfully");
        //            return BlocksNames;
        //        }
        //    }
        //    _Logger.WriteLog("BlocksNames didn't return");
        //    return new ObservableCollection<string>();
        //}

        ///// <summary>Метод для отображения списка строк в таблице</summary>
        //public ObservableCollection<string> LinesFromDataToView(HashSet<Diagnosis> DataCollection, string CurrentDiagnosis, string CurrentBlock)
        //{
        //    _Logger.WriteLog("INFO");

        //    if (CurrentDiagnosis == null ||
        //        CurrentBlock == null)
        //    {
        //        _Logger.WriteLog("BlocksNames or CurrentDiagnosis or CurrentBlock is null");
        //        return new ObservableCollection<string>();
        //    }

        //    foreach (Diagnosis diagnosis in DataCollection)
        //    {
        //        if (diagnosis.Code.Equals(CurrentDiagnosis))
        //        {
        //            foreach (Block block in diagnosis.Blocks)
        //            {
        //                if(block.Name.Equals(CurrentBlock))
        //                {
        //                    var LinesNames = new ObservableCollection<string>(block.Lines);
        //                    _Logger.WriteLog("Lines returned succesfully");
        //                    return LinesNames;
        //                }
        //            }
        //        }
        //    }
        //    _Logger.WriteLog("Lines didn't return");
        //    return new ObservableCollection<string>();
        //}

        ///// <summary>Метод для удаления элемента из базы данных</summary>
        ///// <param name="FocusedDataGrid">Имя выбранного датагрида</param>
        ///// <param name="MultiBox">Содержимое мультибокса</param>
        //public HashSet<Diagnosis> RemoveElement(
        //    string FocusedDataGrid,
        //    string MultiBox,
        //    HashSet<Diagnosis> DataCollection,
        //    string CurrentDiagnosis,
        //    string CurrentBlock,
        //    string CurrentLine)
        //{
        //    _Logger.WriteLog("INFO");

        //    foreach (Diagnosis diagnosis in DataCollection)
        //    {
        //        if (FocusedDataGrid.Equals("Diagnosis"))
        //        {
        //            if (MultiBox.Equals(CurrentDiagnosis) &&
        //                MultiBox.Equals(diagnosis.Code))
        //            {
        //                DataCollection.Remove(diagnosis);
        //                _Logger.WriteLog("Diagnosis removed from DataCollection");
        //                return DataCollection;
        //            }
        //        }
        //        else
        //        {
        //            foreach (Block block in diagnosis.Blocks)
        //            {
        //                if (FocusedDataGrid.Equals("Blocks"))
        //                {
        //                    if (MultiBox.Equals(CurrentBlock) &&
        //                        MultiBox.Equals(block.Name))
        //                    {
        //                        diagnosis.Blocks.Remove(block);
        //                        _Logger.WriteLog("Block removed from DataCollection succesfully");
        //                        return DataCollection;
        //                    }
        //                }
        //                else
        //                {
        //                    foreach (string line in block.Lines)
        //                    {
        //                        if (FocusedDataGrid.Equals("Lines") &&
        //                            line.Equals(MultiBox) &&
        //                            line.Equals(CurrentLine))
        //                        {
        //                            block.Lines.Remove(line);
        //                            _Logger.WriteLog("Line removed from DataCollection succesfully");
        //                            return DataCollection;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    _Logger.WriteLog("Remove filed");
        //    return DataCollection;
        //}

        //public bool RemoveDiagnosis(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = DataCollection.Remove(new Diagnosis() { Code = MultiBox });
        //    if (result) _Logger.WriteLog("Diagnosis removed succesfully");
        //    else _Logger.WriteLog($"Diagnosis remove failed. There is no \"{MultiBox}\" diagnosis");
        //    return result;
        //}
        //public bool RemoveBlock(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem)
        //{

        //}
        //public bool EditDiagnosis(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = false;
        //    DataCollection.Where(t => 
        //    {
        //        if (t.Code.Equals(CurrentItem))
        //        {
        //            result = true;
        //            return true;
        //        }
        //        else return false;
        //    }).Select(t => t.Code = MultiBox).ToHashSet();
        //    _Logger.WriteLog("Diagnosis code changed succesfully");
        //    return result;
        //}

        //public bool EditBlock(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = false;
        //    DataCollection.SelectMany(t => t.Blocks.Where(i =>
        //    {
        //        if (i.Name.Equals(CurrentItem))
        //        {
        //            result = true;
        //            return true;
        //        }
        //        else return false;
        //    }).Select(i => i.Name = MultiBox)).ToHashSet();
        //    _Logger.WriteLog("Block's name changed succesfully");
        //    return result;
        //}

        //public bool EditLine(HashSet<Diagnosis> DataCollection, string MultiBox, string CurrentItem)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = false;
        //    DataCollection.SelectMany(t => t.Blocks.Where(i =>
        //    {
        //        if (i.Lines.Contains(CurrentItem))
        //        {
        //            result = true;
        //            return true;
        //        }
        //        else return false;
        //    }).Select(c =>
        //    {
        //        c.Lines.Remove(CurrentItem);
        //        c.Lines.Add(MultiBox);
        //        return c;
        //    })).ToHashSet();

        //    _Logger.WriteLog("Line changed succesfully");
        //    return result;
        //}
        ///// <summary>Метод поиска диагноза из базы данных</summary>
        ///// <param name="MultiBox">Содержимое мультибокса</param>
        //public ObservableCollection<string> SearchDiagnosis(string MultiBox, HashSet<Diagnosis> DataCollection)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = false;

        //    var DiagnosisCode = new ObservableCollection<string>();


        //    foreach (Diagnosis diagnosis in DataCollection)
        //    {
        //        if (diagnosis.Code.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            DiagnosisCode.Add(diagnosis.Code);
        //            result = true;
        //        }
        //    }
        //    _Logger.WriteLog($"Search result: {result}");
        //    return DiagnosisCode;

        //    //return new ObservableCollection<string>(DataCollection.Where(t => t.Code.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)).Select(t => t.Code));
        //}

        //public ObservableCollection<string> SearchBlocks(string MultiBox, HashSet<Diagnosis> DataCollection)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = false;

        //    var BlocksNames = new ObservableCollection<string>();

        //    foreach (Diagnosis diagnosis in DataCollection)
        //    {
        //        foreach (Block block in diagnosis.Blocks)
        //        {
        //            if (block.Name.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
        //            {
        //                BlocksNames.Add(block.Name);
        //                result = true;
        //            }
        //        }
        //    }


        //    _Logger.WriteLog($"Search result: {result}");
        //    return BlocksNames;
        //    //var temp = DataCollection.SelectMany(t => t.Blocks.Where(t => t.Name.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)).Select(i => i.Name));

        //    //var temp1 = from diagnosis in DataCollection
        //    //            from blocks in diagnosis.Blocks
        //    //            where blocks.Name.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
        //    //            select blocks.Name;
        //}

        //public ObservableCollection<string> SearchLines(string MultiBox, HashSet<Diagnosis> DataCollection)
        //{
        //    _Logger.WriteLog("INFO");
        //    bool result = false;

        //    var LinesNames = new ObservableCollection<string>();

        //    foreach (Diagnosis diagnosis in DataCollection)
        //    {
        //        foreach (Block block in diagnosis.Blocks)
        //        {
        //            foreach (string line in block.Lines)
        //            {
        //                if (line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
        //                {
        //                    LinesNames.Add(line);
        //                    result = true;
        //                }
        //            }
        //        }
        //    }

        //    //var temp = DataCollection.SelectMany(t => t.Blocks.SelectMany(i => i.Lines.Where(a => a.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)).Select(x => x)));

        //    //var temp2 = from diagnosis in DataCollection
        //    //            from blocks in diagnosis.Blocks
        //    //            from line in blocks.Lines
        //    //            where line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase)
        //    //            select line;

        //    _Logger.WriteLog($"Search result: {result}");
        //    return LinesNames;


        //}


        ///// <summary>Метод изменения отображения данных по щелчку</summary>
        ///// <param name="FocusedDataGrid">Имя выбранного датагрида</param>
        ///// <param name="CurrentItem">Содержимое выбранного элемента</param>
        //public void SelectedData(string FocusedDataGrid, string CurrentItem)
        //{
        //    _Logger.WriteLog("INFO");

        //    switch (FocusedDataGrid)
        //    {
        //        case "Diagnosis":
        //            CurrentDiagnosis = CurrentItem;
        //            BlocksFromDataToView();
        //            break;
        //        case "Blocks":
        //            CurrentBlock = CurrentItem;
        //            LinesFromDataToView();
        //            break;
        //        case "Lines":
        //            CurrentLine = CurrentItem;
        //            break;
        //    }
        //    _Logger.WriteLog("DONE");
        //}
        //#endregion

        ///// <summary>Метод для добавления нового элемента</summary>
        ///// <param name="FocusedDataGrid">Датагрида, в который добавляется элемент</param>
        ///// <param name="MultiBox">Элемент, который нужно добавить</param>
        //public void AddELement(string FocusedDataGrid, string MultiBox)
        //{
        //    _Logger.WriteLog("INFO");

        //    if (CheckForDoubles(FocusedDataGrid, MultiBox)) return;

        //    if(FocusedDataGrid.Equals("Diagnosis"))
        //    {
        //        DataCollection.Add(new Diagnosis { Code = MultiBox, Blocks = new HashSet<Block>() });
        //        DiagnosisFromDataToView();
        //        return;
        //    }
        //    else
        //    {
        //        foreach (Diagnosis diagnosis in DataCollection)
        //        {
        //            if(FocusedDataGrid.Equals("Blocks"))
        //            {
        //                if (diagnosis.Code.Equals(CurrentDiagnosis))
        //                {
        //                    diagnosis.Blocks.Add(new Block { Name = MultiBox, Lines = new HashSet<string>() });
        //                    BlocksFromDataToView();
        //                    _Logger.WriteLog("DONE");
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                foreach (Block block in diagnosis.Blocks)
        //                {
        //                    if (block.Name.Equals(CurrentBlock))
        //                    {
        //                        block.Lines.Add(MultiBox);
        //                        LinesFromDataToView();
        //                        _Logger.WriteLog("DONE");
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Метод для поиска двойника
        /// </summary>
        /// <param name="FocusedDataGrid">Выбранны датагрид, в котором осуществляется поиск</param>
        /// <param name="MultiBox">Выбранный для поиска элемент</param>
        /// <returns>True, если элемент найден</returns>
    }
}
