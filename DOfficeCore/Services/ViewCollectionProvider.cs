using DOfficeCore.Logger;
using DOfficeCore.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {
        public ViewCollectionProvider(IViewCollection ViewCollection, ILogger Logger)
        {
            _Logger = Logger;
            _ViewCollection = ViewCollection;
        }

        #region 
        private readonly ILogger _Logger;
        private readonly IViewCollection _ViewCollection;
        #endregion

        #region Методы
        
        /// <summary>Метод создание случайного дневника </summary>
        /// <returns>Случайный дневник</returns>
        public string RandomDiary()
        {
            _Logger.WriteLog("INFO");

            string result = "";
            Random rnd = new Random();
            if (_ViewCollection.CurrentDiagnosis != null)
            {
                foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
                {
                    if(diagnosis.Code.Equals(_ViewCollection.CurrentDiagnosis))
                    {
                        foreach (Block block in diagnosis.Blocks)
                        {
                            result += block.Lines[rnd.Next(block.Lines.Count)] + " ";
                        }
                    }
                }
            }
            _Logger.WriteLog("DONE");

            return result;
        }

        /// <summary>Метод для отображения списка диагнозов в таблице</summary>
        public void DiagnosisFromDataToView()
        {
            _Logger.WriteLog("INFO");

            if (_ViewCollection.DataCollection == null) _ViewCollection.DataCollection = new List<Diagnosis>();
            _ViewCollection.DiagnosisCode = new ObservableCollection<string>(_ViewCollection.DataCollection.
                Select(t => t.Code));
            _Logger.WriteLog("DONE");

        }

        /// <summary>Метод для отображения списка блоков в таблице</summary>
        public void BlocksFromDataToView()
        {
            _Logger.WriteLog("INFO");

            if (_ViewCollection.CurrentDiagnosis == null) return;
            _ViewCollection.BlocksNames = new ObservableCollection<string>();

            foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
            {
                if(diagnosis.Code.Equals(_ViewCollection.CurrentDiagnosis))
                {
                    foreach (Block block in diagnosis.Blocks)
                    {
                        _ViewCollection.BlocksNames.Add(block.Name);
                    }
                    return;
                }
            }
            _Logger.WriteLog("DONE");

        }

        /// <summary>Метод для отображения списка строк в таблице</summary>
        public void LinesFromDataToView()
        {
            _Logger.WriteLog("INFO");

            if (_ViewCollection.BlocksNames == null ||
                _ViewCollection.CurrentDiagnosis == null ||
                _ViewCollection.CurrentBlock == null)
            {
                _Logger.WriteLog("DONE");
                return;
            }

            _ViewCollection.LinesNames = null;

            foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
            {
                if (diagnosis.Code.Equals(_ViewCollection.CurrentDiagnosis))
                {
                    foreach (Block block in diagnosis.Blocks)
                    {
                        if(block.Name.Equals(_ViewCollection.CurrentBlock))
                        {
                            _ViewCollection.LinesNames = new ObservableCollection<string>(block.Lines);
                            _Logger.WriteLog("DONE");
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>Метод для удаления элемента из базы данных</summary>
        /// <param name="FocusedDataGrid">Имя выбранного датагрида</param>
        /// <param name="MultiBox">Содержимое мультибокса</param>
        public void RemoveElement(string FocusedDataGrid,string MultiBox)
        {
            _Logger.WriteLog("INFO");

            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с названием: \"{MultiBox}\"?", "Внимание!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
                {
                    if (FocusedDataGrid.Equals("dgCodes"))
                    {
                        if (MultiBox.Equals(_ViewCollection.CurrentDiagnosis) &&
                            MultiBox.Equals(diagnosis.Code))
                        {
                            _ViewCollection.DataCollection.Remove(diagnosis);
                            DiagnosisFromDataToView();
                            _ViewCollection.BlocksNames = null;
                            _ViewCollection.LinesNames = null;
                            _Logger.WriteLog("DONE");
                            return;
                        }
                    }
                    else 
                    {
                        foreach (Block block in diagnosis.Blocks)
                        {
                            if (FocusedDataGrid.Equals("dgBlocksNames") )
                            {
                                if (MultiBox.Equals(_ViewCollection.CurrentBlock) &&
                                    MultiBox.Equals(block.Name))
                                {
                                    diagnosis.Blocks.Remove(block);
                                    BlocksFromDataToView();
                                    _ViewCollection.LinesNames = null;
                                    _Logger.WriteLog("DONE");
                                    return;
                                }
                            }
                            else
                            {
                                foreach (string line in block.Lines)
                                {
                                    if(FocusedDataGrid.Equals("dgLinesContent") &&
                                        line.Equals(MultiBox) &&
                                        line.Equals(_ViewCollection.CurrentLine))
                                    {
                                        block.Lines.Remove(line);
                                        LinesFromDataToView();
                                        _Logger.WriteLog("DONE");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Метод поиска элемента из базы данных</summary>
        /// <param name="MultiBox">Содержимое мультибокса</param>
        public void SearchElement(string MultiBox)
        {
            _Logger.WriteLog("INFO");

            _ViewCollection.DiagnosisCode = null;
            _ViewCollection.DiagnosisCode = new ObservableCollection<string>();
            _ViewCollection.BlocksNames = null;
            _ViewCollection.BlocksNames = new ObservableCollection<string>();
            _ViewCollection.LinesNames = null;
            _ViewCollection.LinesNames = new ObservableCollection<string>();

            foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
            {
                if (diagnosis.Code.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
                {
                    _ViewCollection.DiagnosisCode.Add(diagnosis.Code);
                }
                else
                {
                    foreach (Block block in diagnosis.Blocks)
                    {
                        if (block.Name.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
                        {
                            _ViewCollection.BlocksNames.Add(block.Name);
                        }
                        else
                        {
                            foreach (string line in block.Lines)
                            {
                                if (line.Contains(MultiBox, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    _ViewCollection.LinesNames.Add(line);
                                }
                            }
                        }
                    }
                }
            }
            if (_ViewCollection.DiagnosisCode.Count == 0) DiagnosisFromDataToView();
            if (_ViewCollection.BlocksNames.Count == 0) BlocksFromDataToView();
            if (_ViewCollection.LinesNames.Count == 0) LinesFromDataToView();
            _Logger.WriteLog("DONE");
        }

        /// <summary>Метод для редактирования элемента базы данных</summary>
        /// <param name="FocusedDataGrid">Имя выбранного датагрида</param>
        /// <param name="MultiBox">Содержимое мультибокса</param>
        public void EditElement(string FocusedDataGrid, string MultiBox)
        {
            _Logger.WriteLog("INFO");

            if (CheckForDoubles(FocusedDataGrid, MultiBox)) return;

            foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
            {
                if (FocusedDataGrid == "dgCodes")
                {
                    if (!diagnosis.Code.Equals(MultiBox) &&
                    diagnosis.Code.Equals(_ViewCollection.CurrentDiagnosis) &&
                    !MultiBox.Equals(_ViewCollection.CurrentDiagnosis))
                    {
                        diagnosis.Code = MultiBox;
                        _ViewCollection.CurrentDiagnosis = MultiBox;
                        DiagnosisFromDataToView();
                        _Logger.WriteLog("DONE");
                        return;
                    }
                }
                else
                {
                    foreach (Block block in diagnosis.Blocks)
                    {
                        if (FocusedDataGrid == "dgBlocksNames")
                        {
                            if (!block.Name.Equals(MultiBox) &&
                            block.Name.Equals(_ViewCollection.CurrentBlock) &&
                            !MultiBox.Equals(_ViewCollection.CurrentBlock))
                            {
                                block.Name = MultiBox;
                                _ViewCollection.CurrentBlock = MultiBox;
                                BlocksFromDataToView();
                                _Logger.WriteLog("DONE");
                                return;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < block.Lines.Count; i++)
                            {
                                if (!block.Lines[i].Equals(MultiBox) &&
                                    block.Lines[i].Equals(_ViewCollection.CurrentLine) &&
                                    FocusedDataGrid == "dgLinesContent" &&
                                    !MultiBox.Equals(_ViewCollection.CurrentLine))
                                {
                                    block.Lines.RemoveAt(i);
                                    block.Lines.Add(MultiBox);
                                    LinesFromDataToView();
                                    _Logger.WriteLog("DONE");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Метод изменения отображения данных по щелчку</summary>
        /// <param name="FocusedDataGrid">Имя выбранного датагрида</param>
        /// <param name="CurrentItem">Содержимое выбранного элемента</param>
        public void SelectedData(string FocusedDataGrid, string CurrentItem)
        {
            _Logger.WriteLog("INFO");

            switch (FocusedDataGrid)
            {
                case "dgCodes":
                    _ViewCollection.CurrentDiagnosis = CurrentItem;
                    BlocksFromDataToView();
                    break;
                case "dgBlocksNames":
                    _ViewCollection.CurrentBlock = CurrentItem;
                    LinesFromDataToView();
                    break;
                case "dgLinesContent":
                    _ViewCollection.CurrentLine = CurrentItem;
                    break;
            }
            _Logger.WriteLog("DONE");
        }
        #endregion

        /// <summary>Метод для добавления нового элемента</summary>
        /// <param name="FocusedDataGrid">Датагрида, в который добавляется элемент</param>
        /// <param name="MultiBox">Элемент, который нужно добавить</param>
        public void AddELement(string FocusedDataGrid, string MultiBox)
        {
            _Logger.WriteLog("INFO");

            if (CheckForDoubles(FocusedDataGrid, MultiBox)) return;

            if(FocusedDataGrid.Equals("dgCodes"))
            {
                _ViewCollection.DataCollection.Add(new Diagnosis { Code = MultiBox, Blocks = new List<Block>() });
                DiagnosisFromDataToView();
                return;
            }
            else
            {
                foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
                {
                    if(FocusedDataGrid.Equals("dgBlocksNames"))
                    {
                        if (diagnosis.Code.Equals(_ViewCollection.CurrentDiagnosis))
                        {
                            diagnosis.Blocks.Add(new Block { Name = MultiBox, Lines = new List<string>() });
                            BlocksFromDataToView();
                            _Logger.WriteLog("DONE");
                            return;
                        }
                    }
                    else
                    {
                        foreach (Block block in diagnosis.Blocks)
                        {
                            if (block.Name.Equals(_ViewCollection.CurrentBlock))
                            {
                                block.Lines.Add(MultiBox);
                                LinesFromDataToView();
                                _Logger.WriteLog("DONE");
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод для поиска двойника
        /// </summary>
        /// <param name="FocusedDataGrid">Выбранны датагрид, в котором осуществляется поиск</param>
        /// <param name="MultiBox">Выбранный для поиска элемент</param>
        /// <returns>True, если элемент найден</returns>
        private bool CheckForDoubles(string FocusedDataGrid, string MultiBox)
        {
            _Logger.WriteLog("INFO");

            foreach (Diagnosis diagnosis in _ViewCollection.DataCollection)
            {
                if (FocusedDataGrid.Equals("dgCodes"))
                {
                    if (diagnosis.Code.Equals(MultiBox))
                    {
                        MessageBox.Show("Элемент с таким названием уже существует.");
                        _Logger.WriteLog("DONE");
                        return true;
                    }
                }
                else
                {
                    foreach (Block block in diagnosis.Blocks)
                    {
                        if (FocusedDataGrid.Equals("dgBlocksNames"))
                        {
                            if (block.Name.Equals(MultiBox))
                            {
                                MessageBox.Show("Элемент с таким названием уже существует.");
                                _Logger.WriteLog("DONE");
                                return true;
                            }
                        }
                        else
                        {
                            foreach (string line in block.Lines)
                            {
                                if (line.Equals(MultiBox))
                                {
                                    MessageBox.Show("Элемент с таким названием уже существует.");
                                    _Logger.WriteLog("DONE");
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            _Logger.WriteLog("DONE");
            return false;
        }

    }
}
