﻿using DOfficeCore.Data;
using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Logger;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(IDataProviderService DataProviderService, 
                                    IViewCollectionProvider ViewCollectionProvider, 
                                    IDiaryBoxProvider DiaryBoxProvider,
                                    ILogger Logger,
                                    ILineEditorService LineEditorService)
        {
            _DataProviderService = DataProviderService;
            _ViewCollectionProvider = ViewCollectionProvider;
            _DiaryBoxProvider = DiaryBoxProvider;
            _LineEditorService = LineEditorService;
            _Logger = Logger;

            _Logger.WriteLog("INFO", "Создание MainWindowViewModel");

            #region Команды окна дневника

            EditTextCommand = new LambdaCommand(OnEditTextCommandExecuted, CanEditTextCommandExecute);
            CopyTextCommand = new LambdaCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
            SaveDataToFileCommand = new LambdaCommand(OnSaveDataToFileCommandExecuted, CanSaveDataToFileCommandExecute);
            LoadDataCommand = new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
            SelectedDataCommand = new LambdaCommand(OnSelectedDataCommandExecuted, CanSelectedDataCommandExecute);
            EditElementCommand = new LambdaCommand(OnEditElementCommandExecuted, CanEditElementCommandExecute);
            StringTransferCommand = new LambdaCommand(OnStringTransferCommandExecuted, CanStringTransferCommandExecute);
            SearchElementCommand = new LambdaCommand(OnSearchElementCommandExecuted, CanSearchElementCommandExecute);
            RemoveElementCommand = new LambdaCommand(OnRemoveElementCommandExecuted, CanRemoveElementCommandExecute);
            AddElementCommand = new LambdaCommand(OnAddElementCommandExecuted, CanAddElementCommandExecute);
            AddTimeCommand = new LambdaCommand(OnAddTimeCommandExecuted, CanAddTimeCommandExecute);
            AddDateCommand = new LambdaCommand(OnAddDateCommandExecuted, CanAddDateCommandExecute);
            ClearDiaryBoxCommand = new LambdaCommand(OnClearDiaryBoxCommandExecuted, CanClearDiaryBoxCommandExecute);
            AddDoctorCommand = new LambdaCommand(OnAddDoctorCommandExecuted, CanAddDoctorCommandExecute);
            DeleteDoctorCommand = new LambdaCommand(OnDeleteDoctorCommandExecuted, CanDeleteDoctorCommandExecute);
            EditDoctorCommand = new LambdaCommand(OnEditDoctorCommandExecuted, CanEditDoctorCommandExecute);
            AddPositionCommand = new LambdaCommand(OnAddPositionCommandExecuted, CanAddPositionCommandExecute);
            DeletePositionCommand = new LambdaCommand(OnDeletePositionCommandExecuted, CanDeletePositionCommandExecute);
            EditPositionCommand = new LambdaCommand(OnEditPositionCommandExecuted, CanEditPositionCommandExecute);
            SaveDoctorsListCommand = new LambdaCommand(OnSaveDoctorsListCommandExecuted, CanSaveDoctorsListCommandExecute);
            AddDocToDiaryCommand = new LambdaCommand(OnAddDocToDiaryCommandExecuted, CanAddDocToDiaryCommandExecute);
            RandomCommand = new LambdaCommand(OnRandomCommandExecuted, CanRandomCommandExecute);
            ClosingAppCommand = new LambdaCommand(OnClosingAppCommandExecuted, CanClosingAppCommandExecute);
            #endregion

            #region Команды окна редактирования строк

            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            GetTextFromClipboardCommand = new LambdaCommand(OnGetTextFromClipboardCommandExecuted, CanGetTextFromClipboardCommandExecute);
            ClearListBoxCommand = new LambdaCommand(OnClearListBoxCommandExecuted, CanClearListBoxCommandExecute);
            AddNewDiagnosisCommand = new LambdaCommand(OnAddNewDiagnosisCommandExecuted, CanAddNewDiagnosisCommandExecute);
            RemoveDiagnosisCommand = new LambdaCommand(OnRemoveDiagnosisCommandExecuted, CanRemoveDiagnosisCommandExecute);

            #endregion
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

        #region DataCollection : HashSet<Diagnosis> - Коллекция данных из базы данных

        /// <summary>Коллекция данных из базы данных</summary>
        private HashSet<Diagnosis> _DataCollection;

        /// <summary>Коллекция данных из базы данных</summary>
        public HashSet<Diagnosis> DataCollection
        {
            get => _DataCollection;
            set => Set(ref _DataCollection, value);
        }

        #endregion

        #region DiagnosisCode : ObservableCollection<string> - Коллекция кодов диагнозов

        /// <summary>Коллекция кодов диагнозов</summary>
        private ObservableCollection<string> _DiagnosisCode;

        /// <summary>Коллекция кодов диагнозов</summary>
        public ObservableCollection<string> DiagnosisCode
        {
            get => _DiagnosisCode;
            set => Set(ref _DiagnosisCode, value);
        }

        #endregion

        #region BlocksNames : ObservableCollection<string> - Коллекция названий блоков

        /// <summary>Коллекция названий блоков</summary>
        private ObservableCollection<string> _BlocksNames;

        /// <summary>Коллекция названий блоков</summary>
        public ObservableCollection<string> BlocksNames
        {
            get => _BlocksNames;
            set => Set(ref _BlocksNames, value);
        }

        #endregion

        #region LinesNames : ObservableCollection<string> - Коллекция содержимого строк

        /// <summary>Коллекция содержимого строк</summary>
        private ObservableCollection<string> _LinesNames;

        /// <summary>Коллекция содержимого строк</summary>
        public ObservableCollection<string> LinesNames
        {
            get => _LinesNames;
            set => Set(ref _LinesNames, value);
        }

        #endregion

        #region CurrentDiagnosis : string - Текущий выбранный диагноз

        /// <summary>Текущий выбранный диагноз</summary>
        private string _CurrentDiagnosis;

        /// <summary>Текущий выбранный диагноз</summary>
        public string CurrentDiagnosis
        {
            get => _CurrentDiagnosis;
            set => _CurrentDiagnosis = value;
        }

        #endregion

        #region CurrentBlock : string - Текущий выбранный блок

        /// <summary>Текущий выбранный блок</summary>
        private string _CurrentBlock;

        /// <summary>Текущий выбранный блок</summary>
        public string CurrentBlock
        {
            get => _CurrentBlock;
            set => _CurrentBlock = value;
        }

        #endregion

        #region CurrentLine : string - Текущая выбранная строка

        /// <summary>Текущая выбранная строка</summary>
        private string _CurrentLine;

        /// <summary>Текущая выбранная строка</summary>
        public string CurrentLine
        {
            get => _CurrentLine;
            set => _CurrentLine = value;
        }

        #endregion

        #endregion

        #region Команды

        #region Команда загрузки данных
        /// <summary>Команда Загрузки данных</summary>
        public ICommand LoadDataCommand { get; }
        /// <summary>Команда Загрузки данных</summary>
        private void OnLoadDataCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");
            
            var temp = _DataProviderService.LoadDoctorsFromFile("Doctors.json");
            if (temp != null) Doctors = new ObservableCollection<string>(temp);
            temp = _DataProviderService.LoadDoctorsFromFile("Position.json");
            if (temp != null) Position = new ObservableCollection<string>(temp);

            // Тестовые данные
            DataCollection = TestData.Diag;
            DiagnosisCode = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);

            // Реальные данные
            //DataCollection = _DataProviderService.LoadDataFromFile("file.json");
            //DiagnosisCode = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);

            _Logger.WriteLog("DONE");
        }

        private bool CanLoadDataCommandExecute(object parameter) => true;
        #endregion
        
        #region Команда закрытия программы
        /// <summary>Команда закрытия программы</summary>
        public ICommand ClosingAppCommand { get; }
        /// <summary>Команда закрытия программы</summary>
        private void OnClosingAppCommandExecuted(object parameter)
        {
            _Logger.WriteLog("EXIT", "Закрытие программы.");
        }

        private bool CanClosingAppCommandExecute(object parameter) => true;

        #endregion

        #endregion

        #region Сервисы

        #region Сервис обработки строк
        private readonly ILineEditorService _LineEditorService;
        #endregion

        #region Сервис логгирования
        private readonly ILogger _Logger;
        #endregion

        #region Сервис работы с файлами
        private readonly IDataProviderService _DataProviderService;
        #endregion

        #region Сервис работы с данными
        private readonly IViewCollectionProvider _ViewCollectionProvider;
        #endregion

        #region Сервис работы с дневником
        private readonly IDiaryBoxProvider _DiaryBoxProvider;
        #endregion

        #endregion
    }
}
