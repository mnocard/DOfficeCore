using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace DOfficeCore.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Форматы открываемых файлов
        private const string dlgFilter = "Word documents (.docx)|*.docx";
        private const string dlgDefaultExt = ".docx";
        #endregion

        #region Свойства окна обработчика строк

        #region TextForEditing : string - Текст, который необходимо обработать

        /// <summary>Текст, который необходимо обработать</summary>
        private string _TextForEditing;

        /// <summary>Текст, который необходимо обработать</summary>
        public string TextForEditing
        {
            get => _TextForEditing;
            set => Set(ref _TextForEditing, value);
        }

        #endregion

        #region RawLines : ObservableCollection<string> - Необработанная коллекция строк

        /// <summary>Необработанная коллекция строк</summary>
        private ObservableCollection<string> _RawLines;

        /// <summary>Необработанная коллекция строк</summary>
        public ObservableCollection<string> RawLines
        {
            get => _RawLines;
            set => Set(ref _RawLines, value);
        }

        #endregion

        #region DiagnosisTextBox : string - Строка диагноза

        /// <summary>Строка диагноза</summary>
        private string _DiagnosisTextBox;

        /// <summary>Строка диагноза</summary>
        public string DiagnosisTextBox
        {
            get => _DiagnosisTextBox;
            set => Set(ref _DiagnosisTextBox, value);
        }

        #endregion

        #region BlockTextBox : string - Строка раздела

        /// <summary>Строка раздела</summary>
        private string _BlockTextBox;

        /// <summary>Строка раздела</summary>
        public string BlockTextBox
        {
            get => _BlockTextBox;
            set => Set(ref _BlockTextBox, value);
        }

        #endregion

        #endregion

        //#region Команды окна обработчика строк

        //#region Открытие файла
        ///// <summary>Открытие файла</summary>
        //public ICommand OpenFileCommand { get; }
        ///// <summary>Открытие файла</summary>
        //private void OnOpenFileCommandExecuted(object parameter)
        //{
        //    var dlg = new OpenFileDialog();
        //    dlg.DefaultExt = dlgDefaultExt;
        //    dlg.Filter = dlgFilter;

        //    var result = dlg.ShowDialog();

        //    if (result == true)
        //    {
        //        TextForEditing = _LineEditorService.OpenDocument(dlg.FileName);
        //        if (RawLines == null) RawLines = new ObservableCollection<string>();
        //        foreach (var item in _LineEditorService.TextToLines(TextForEditing))
        //        {
        //            RawLines.Add(item);
        //        }
        //    }
        //}

        //private bool CanOpenFileCommandExecute(object parameter) => true;

        //#endregion

        //#region Получение текста из буфера обмена
        ///// <summary>Получение текста из буфера обмена</summary>
        //public ICommand GetTextFromClipboardCommand { get; }
        ///// <summary>Получение текста из буфера обмена</summary>
        //private void OnGetTextFromClipboardCommandExecuted(object parameter)
        //{
        //    if (Clipboard.ContainsText())
        //    {
        //        TextForEditing = Clipboard.GetText();
        //        if (RawLines == null) RawLines = new ObservableCollection<string>();
        //        foreach (var item in _LineEditorService.TextToLines(TextForEditing))
        //        {
        //            RawLines.Add(item);
        //        }
        //    }
        //}

        //private bool CanGetTextFromClipboardCommandExecute(object parameter) => true;

        //#endregion

        //#region Удаление всех элементов необработанной таблицы
        ///// <summary>Удаление всех элементов необработанной таблицы</summary>
        //public ICommand ClearListBoxCommand { get; }
        ///// <summary>Удаление всех элементов необработанной таблицы</summary>
        //private void OnClearListBoxCommandExecuted(object parameter)
        //{
        //    RawLines = null;
        //}

        //private bool CanClearListBoxCommandExecute(object parameter) => RawLines != null;

        //#endregion

        //#region Добавление нового диагноза
        ///// <summary>Добавление нового диагноза</summary>
        //public ICommand AddNewDiagnosisCommand { get; }
        ///// <summary>Добавление нового диагноза</summary>
        //private void OnAddNewDiagnosisCommandExecuted(object parameter)
        //{
        //    _Logger.WriteLog("INFO");

        //    if (DiagnosisTextBox != null)
        //    {
        //        _ViewCollectionProvider.AddELement("dgCodes", DiagnosisTextBox);
        //    }

        //    _Logger.WriteLog("DONE");
        //}

        //private bool CanAddNewDiagnosisCommandExecute(object parameter) => true;

        //#endregion

        //#region Удаление диагноза
        ///// <summary>Удаление диагноза</summary>
        //public ICommand RemoveDiagnosisCommand { get; }
        ///// <summary>Удаление диагноза</summary>
        //private void OnRemoveDiagnosisCommandExecuted(object parameter)
        //{
        //    throw new NotImplementedException();
        //}

        //private bool CanRemoveDiagnosisCommandExecute(object parameter) => true;

        //#endregion

        //#endregion
    }
}
