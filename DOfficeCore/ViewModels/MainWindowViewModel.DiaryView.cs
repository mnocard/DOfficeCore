﻿using System;
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


        #region Щелчок по элементу списка диагнозов
        /// <summary>Щелчок по элементу списка диагнозов</summary>
        public ICommand SelectedDiagnosisCommand { get; }
        /// <summary>Щелчок по элементу списка диагнозов</summary>
        private void OnSelectedDiagnosisCommandExecuted(object parameter)
        {
            if (parameter is Section CurrentItem)
            {
                CurrentSection = Section.CloneSection(CurrentItem);
                BlocksList = _ViewCollectionProvider.BlocksFromDataToView(DataCollection, CurrentSection);
                LinesList = new ObservableCollection<Section>();
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
            if (parameter is Section CurrentItem)
            {
                CurrentSection = Section.CloneSection(CurrentItem);
                LinesList = _ViewCollectionProvider.LinesFromDataToView(DataCollection, CurrentSection);
            }
        }
        private bool CanSelectedBlockCommandExecute(object parameter) => true;

        #endregion

        #region Щелчок по элементу списка строк
        /// <summary>Щелчок по элементу списка строк</summary>
        public ICommand SelectedLineCommand { get; }
        /// <summary>Щелчок по элементу списка строк</summary>
        private void OnSelectedLineCommandExecuted(object parameter)
        {
            if (parameter is Section CurrentItem)
            {
                CurrentSection = Section.CloneSection(CurrentItem);
                DiaryBox = _DiaryBoxProvider.LineToDiaryBox(DiaryBox, CurrentSection.Line);
            }
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
            if (CurrentSection != null)
            {
                DiaryBox = _ViewCollectionProvider.RandomDiary(DataCollection, CurrentSection);
                Status = "Случайный дневник создан согласно записям: " + CurrentSection.Diagnosis;
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
            if (parameter is string temp && !string.IsNullOrWhiteSpace(temp))
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

        #endregion
    }
}
