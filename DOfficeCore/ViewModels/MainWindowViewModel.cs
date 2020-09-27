using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.ViewModels.Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    internal class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel()
        {
            #region Команды
            EditTextCommand = new LambdaCommand(OnEditTextCommandExecuted, CanEditTextCommandExecute);
            CopyTextCommand = new LambdaCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
            SaveDataToFileCommand = new LambdaCommand(OnSaveDataToFileCommandExecuted, CanSaveDataToFileCommandExecute);
            #endregion

            Diagnoses = DataProviderService.LoadDataFromFile("file.json");
        }

        #region Свойства
        #region Заголовок окна
        /// <summary>Заголовок окна</summary>
        private string _Title = "Кабинет врача";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Редактирование текста
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        private bool _EnableTextBox = true;
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        public bool EnableTextBox
        {
            get => _EnableTextBox;
            set => Set(ref _EnableTextBox, value);
        }
        #endregion

        #region Коллекция данных
        /// <summary>Коллекция данных для отправки в дерево</summary>
        private ObservableCollection<Diagnosis> _Diagnoses;
        /// <summary>Коллекция данных для отправки в дерево</summary>
        public ObservableCollection<Diagnosis> Diagnoses
        {
            get => _Diagnoses;
            set => Set(ref _Diagnoses, value);
        }
        #endregion

        #endregion

        #region Команды

        #region Команда редактирования текста
        public ICommand EditTextCommand { get; }

        private void OnEditTextCommandExecuted(object p)
        {
            if (EnableTextBox) EnableTextBox = false;
        }

        private bool CanEditTextCommandExecute(object p)
        {
            return EnableTextBox;
        }
        #endregion

        #region Команда копирования текста
        public ICommand CopyTextCommand { get; }

        private void OnCopyTextCommandExecuted(object parameter)
        {
            if (parameter as string != null)
            {
                Clipboard.SetText(parameter as string);
                EnableTextBox = true;
            }
        }

        private bool CanCopyTextCommandExecute(object parameter) => true;
        #endregion

        #region Команда сохранения данных в файл
        public ICommand SaveDataToFileCommand { get; }

        private void OnSaveDataToFileCommandExecuted(object p)
        {
            if (p as IEnumerable != null)
            {
                if(DataProviderService.SaveDataToFile((IEnumerable)p, "file"))
                {
                    MessageBox.Show("Файл успешно сохранён.");
                }
            }
        }

        private bool CanSaveDataToFileCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #endregion
    }
}
