using DOfficeCore.Data;
using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Logger;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(IDataProviderService DataProviderService, 
                                    IViewCollectionProvider ViewCollectionProvider, 
                                    IViewCollection ViewCollection,
                                    IDiaryBoxProvider DiaryBoxProvider,
                                    ILogger Logger,
                                    ILineEditorService LineEditorService)
        {
            _DataProviderService = DataProviderService;
            _ViewCollectionProvider = ViewCollectionProvider;
            _ViewCollection = ViewCollection;
            _DiaryBoxProvider = DiaryBoxProvider;
            _LineEditorService = LineEditorService;
            _Logger = Logger;

            _Logger.WriteLog("INFO", "Создание MainWindowViewModel");

            #region Команды

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

            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            GetTextFromClipboardCommand = new LambdaCommand(OnGetTextFromClipboardCommandExecuted, CanGetTextFromClipboardCommandExecute);
            ClearListBoxCommand = new LambdaCommand(OnClearListBoxCommandExecuted, CanClearListBoxCommandExecute);
            AddNewDiagnosisCommand = new LambdaCommand(OnAddNewDiagnosisCommandExecuted, CanAddNewDiagnosisCommandExecute);
            RemoveDiagnosisCommand = new LambdaCommand(OnRemoveDiagnosisCommandExecuted, CanRemoveDiagnosisCommandExecute);

            #endregion
        }

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
            _ViewCollection.DataCollection = TestData.Diag;
            _ViewCollectionProvider.DiagnosisFromDataToView();

            // Реальные данные
            //_ViewCollection.DataCollection = _DataProviderService.LoadDataFromFile("file.json");
            //_ViewCollectionProvider.DiagnosisFromDataToView();

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

        #region ViewCollection : IViewCollection - Коллекция данных
        private readonly IViewCollection _ViewCollection;
        /// <summary>Публичное свойство нужно для привязки представления к коллекции</summary>
        public IViewCollection ViewCollection { get => _ViewCollection; }
        #endregion

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
