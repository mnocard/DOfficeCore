using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using DOfficeCore.Models;

namespace DOfficeCore.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Свойства окна дневника

        #region EnableDiaryBox : bool - Состояние возможности редактирования текстового окна
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        private bool _EnableDiaryBox = true;
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        public bool EnableDiaryBox
        {
            get => _EnableDiaryBox;
            set => Set(ref _EnableDiaryBox, value);
        }
        #endregion

        #region MultiBox : string - Содержимое мультибокса

        /// <summary>Содержимое мультибокса</summary>
        private string _MultiBox;

        /// <summary>Содержимое мультибокса</summary>
        public string MultiBox
        {
            get => _MultiBox;
            set => Set(ref _MultiBox, value);
        }

        #endregion

        #region DiaryBox : string - Содержимое дневника

        /// <summary>Содержимое дневника</summary>
        private string _DiaryBox;

        /// <summary>Содержимое дневника</summary>
        public string DiaryBox
        {
            get => _DiaryBox;
            set => Set(ref _DiaryBox, value);
        }

        #endregion

        #endregion

        #region Команды вкладки дневника

        #region Щелчок по элементу списка диагнозов
        /// <summary>Щелчок по элементу списка диагнозов</summary>
        public ICommand SelectedDiagnosisCommand { get; }
        /// <summary>Щелчок по элементу списка диагнозов</summary>
        private void OnSelectedDiagnosisCommandExecuted(object parameter)
        {
            if (parameter is Sector sector)
            {
                BlocksList = new ObservableCollection<Block>(sector.Blocks);
                LinesList = new ObservableCollection<string>();
            }
        }
        private bool CanSelectedDiagnosisCommandExecute(object parameter) => true;

        #endregion

        #region Щелчок по элементу списка разделов
        /// <summary>Щелчок по элементу списка разделов</summary>
        public ICommand SelectedBlockCommand { get; }
        /// <summary>Щелчок по элементу списка разделов</summary>
        private void OnSelectedBlockCommandExecuted(object parameter)
        {
            if (parameter is Block block)
                LinesList = new ObservableCollection<string>(block.Lines);
        }
        private bool CanSelectedBlockCommandExecute(object parameter) => true;

        #endregion

        #region Щелчок по элементу списка строк
        /// <summary>Щелчок по элементу списка строк</summary>
        public ICommand SelectedLineCommand { get; }
        /// <summary>Щелчок по элементу списка строк</summary>
        private void OnSelectedLineCommandExecuted(object parameter)
        {
            if (parameter is string line)
                DiaryBox = _DiaryBoxProvider.LineToDiaryBox(DiaryBox, line);
        }

        private bool CanSelectedLineCommandExecute(object parameter) => true;

        #endregion

        #region Команда поиска элементов
        /// <summary>Команда поиска элементов</summary>
        public ICommand SearchElementCommand { get; }
        /// <summary>Команда поиска элементов</summary>
        private void OnSearchElementCommandExecuted(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(MultiBox) && MultiBox.Length >= 3)
            {
                DiagnosisList = _ViewCollectionProvider.SearchDiagnosis(DataCollection, MultiBox);
                if (DiagnosisList.Count == 0) DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
                BlocksList = _ViewCollectionProvider.SearchBlocks(DataCollection, MultiBox);
                LinesList = _ViewCollectionProvider.SearchLines(DataCollection, MultiBox);
                Status = "Вот, что мы нашли по вашему запросу";
            }
            else
            {
                Status = "Введите не менее трёх символов для поиска";
                DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
            }
        }

        private bool CanSearchElementCommandExecute(object parameter) => true;

        #endregion

        #region Создание случайного дневника
        /// <summary>Создание случайного дневника</summary>
        public ICommand RandomCommand { get; }
        /// <summary>Создание случайного дневника</summary>
        private void OnRandomCommandExecuted(object parameter)
        {
            if (BlocksList.Any())
            {
                (DiaryBox, LinesList) = _DiaryBoxProvider.RandomDiaryWithNewModel(BlocksList);
                Status = "Случайный дневник создан согласно записям: " + BlocksList[0].Sector;
            }
        }

        private bool CanRandomCommandExecute(object parameter) => true;

        #endregion

        #region Команда копирования текста
        /// <summary>Команда копирования текста</summary>
        public ICommand CopyTextCommand { get; }
        /// <summary>Команда копирования текста</summary>
        private void OnCopyTextCommandExecuted(object parameter)
        {
            if (parameter is string temp && !string.IsNullOrWhiteSpace(DiaryBox))
            {
                Clipboard.SetText(temp);
                EnableDiaryBox = true;
                Status = "Дневник скопирован в буфер обмена";
            }
            else Status = "Что-то пошло не так";
        }
        private bool CanCopyTextCommandExecute(object parameter) => parameter is string temp && temp != string.Empty && temp != "";
        #endregion

        #region Команда редактирования текста в окне дневника
        /// <summary>Команда редактирования текста в окне дневника</summary>
        public ICommand EditTextCommand { get; }
        /// <summary>Команда редактирования текста в окне дневника</summary>
        private void OnEditTextCommandExecuted(object p)
        {
            if (EnableDiaryBox)
            {
                EnableDiaryBox = false;
                Status = "Теперь вы можете сами отредактировать дневник";
            }
        }

        private bool CanEditTextCommandExecute(object p) => EnableDiaryBox;
        #endregion

        #region Команда очистки дневника
        /// <summary>Команда очистки дневника</summary>
        public ICommand ClearDiaryBoxCommand { get; }
        /// <summary>Команда очистки дневника</summary>
        private void OnClearDiaryBoxCommandExecuted(object parameter)
        {
            DiaryBox = string.Empty;
            EnableDiaryBox = true;
            Status = "Начинаем с чистого листа";
        }

        private bool CanClearDiaryBoxCommandExecute(object parameter) => true;

        #endregion

        #region Команда переноса текста из дневника в коллекцию
        /// <summary>Команда переноса текста из дневника в коллекцию</summary>
        public ICommand ReturnTextToLinesCommand { get; }
        /// <summary>Команда переноса текста из дневника в коллекцию</summary>
        private void OnReturnTextToLinesCommandExecuted(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(DiaryBox))
            {
                var lines = _LineEditorService.TextToLines(DiaryBox);
                LinesList = new ObservableCollection<string>(lines);
                RawLines = new ObservableCollection<string>(lines);
            }
        }

        private bool CanReturnTextToLinesCommandExecute(object parameter) => true;

        #endregion

        #endregion
    }
}
