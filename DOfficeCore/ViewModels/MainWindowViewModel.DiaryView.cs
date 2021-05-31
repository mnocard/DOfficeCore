using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

        #region CurrentSection : Section - Выбранная в датагриде секция

        /// <summary>Выбранная в датагриде секция</summary>
        private Section _CurrentSection;

        /// <summary>Выбранная в датагриде секция</summary>
        public Section CurrentSection
        {
            get => _CurrentSection;
            set => Set(ref _CurrentSection, value);
        }

        #endregion

        #endregion

        #region Команды вкладки дневника

        #region Изменение отображения данных по щелчку
        /// <summary>Изменение отображения данных по щелчку</summary>
        public ICommand SelectedDataCommand { get; }
        /// <summary>Изменение отображения данных по щелчку</summary>
        private void OnSelectedDataCommandExecuted(object parameter)
        {
            if ((parameter is ListBox listBox) && listBox.SelectedItem is Section CurrentItem)
            {
                CurrentSection = CurrentItem;
                switch (listBox.Name)
                {
                    
                    case "dgCodes": 
                        BlocksList = _ViewCollectionProvider.BlocksFromDataToView(DataCollection, CurrentSection);
                        LinesList = new ObservableCollection<Section>();
                        break;
                    
                    case "dgBlocksNames":
                        LinesList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, CurrentSection);
                        break;
                    case "dgLinesContent":
                        DiaryBox = _DiaryBoxProvider.LineToDiaryBox(DiaryBox, CurrentSection.Line);
                        break;
                    
                    default:
                        break;
                }
            }
        }

        private bool CanSelectedDataCommandExecute(object parameter) => parameter != null;

        #endregion

        #region Команда поиска элементов
        /// <summary>Команда поиска элементов</summary>
        public ICommand SearchElementCommand { get; }
        /// <summary>Команда поиска элементов</summary>
        private void OnSearchElementCommandExecuted(object parameter)
        {
            if (MultiBox != null && MultiBox != string.Empty && MultiBox.Length >= 3)
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
            if ((parameter is ListBox) && CurrentSection != null)
            {
                DiaryBox = _ViewCollectionProvider.RandomDiary(DataCollection, CurrentSection);
                Status = "Случайный дневник создан согласно записям диагноза: " + CurrentSection.Diagnosis;
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
            if (parameter is string temp && temp != string.Empty && temp != "")
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
            DiaryBox = null;
            EnableDiaryBox = true;
            Status = "Начинаем с чистого листа";
        }

        private bool CanClearDiaryBoxCommandExecute(object parameter) => true;

        #endregion
        
        #endregion
    }
}
