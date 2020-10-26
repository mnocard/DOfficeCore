using DOfficeCore.Data;
using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Logger;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels.Core;
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
            LoadDataCommand = new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
            ClosingAppCommand = new LambdaCommand(OnClosingAppCommandExecuted, CanClosingAppCommandExecute);

            SelectedDataCommand = new LambdaCommand(OnSelectedDataCommandExecuted, CanSelectedDataCommandExecute);
            
            AddDateCommand = new LambdaCommand(OnAddDateCommandExecuted, CanAddDateCommandExecute);
            AddTimeCommand = new LambdaCommand(OnAddTimeCommandExecuted, CanAddTimeCommandExecute);
            
            AddDocToDiaryCommand = new LambdaCommand(OnAddDocToDiaryCommandExecuted, CanAddDocToDiaryCommandExecute);
            AddPositionCommand = new LambdaCommand(OnAddPositionCommandExecuted, CanAddPositionCommandExecute);
            EditPositionCommand = new LambdaCommand(OnEditPositionCommandExecuted, CanEditPositionCommandExecute);
            DeletePositionCommand = new LambdaCommand(OnDeletePositionCommandExecuted, CanDeletePositionCommandExecute); 
            AddDoctorCommand = new LambdaCommand(OnAddDoctorCommandExecuted, CanAddDoctorCommandExecute);
            EditDoctorCommand = new LambdaCommand(OnEditDoctorCommandExecuted, CanEditDoctorCommandExecute);
            DeleteDoctorCommand = new LambdaCommand(OnDeleteDoctorCommandExecuted, CanDeleteDoctorCommandExecute);
            
            SaveDoctorsListCommand = new LambdaCommand(OnSaveDoctorsListCommandExecuted, CanSaveDoctorsListCommandExecute);
            
            SearchElementCommand = new LambdaCommand(OnSearchElementCommandExecuted, CanSearchElementCommandExecute);
            
            RandomCommand = new LambdaCommand(OnRandomCommandExecuted, CanRandomCommandExecute);
            CopyTextCommand = new LambdaCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
            EditTextCommand = new LambdaCommand(OnEditTextCommandExecuted, CanEditTextCommandExecute);
            ClearDiaryBoxCommand = new LambdaCommand(OnClearDiaryBoxCommandExecuted, CanClearDiaryBoxCommandExecute);
            #endregion

            #region Команды окна редактирования строк
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            GetTextFromClipboardCommand = new LambdaCommand(OnGetTextFromClipboardCommandExecuted, CanGetTextFromClipboardCommandExecute); 
            SaveDataToFileCommand = new LambdaCommand(OnSaveDataToFileCommandExecuted, CanSaveDataToFileCommandExecute);
            ClearListBoxCommand = new LambdaCommand(OnClearListBoxCommandExecuted, CanClearListBoxCommandExecute);
            TransferCommand = new LambdaCommand(OnTransferCommandExecuted, CanTransferCommandExecute);

            AddDiagnosisCommand = new LambdaCommand(OnAddDiagnosisCommandExecuted, CanAddDiagnosisCommandExecute);
            AddBlockCommand = new LambdaCommand(OnAddBlockCommandExecuted, CanAddBlockCommandExecute);
            AddLineCommand = new LambdaCommand(OnAddLineCommandExecuted, CanAddLineCommandExecute);
            
            EditDiagnosisCommand = new LambdaCommand(OnEditDiagnosisCommandExecuted, CanEditDiagnosisCommandExecute);
            EditBlockCommand = new LambdaCommand(OnEditBlockCommandExecuted, CanEditBlockCommandExecute);
            EditLineCommand = new LambdaCommand(OnEditLineCommandExecuted, CanEditLineCommandExecute);

            RemoveDiagnosisCommand = new LambdaCommand(OnRemoveDiagnosisCommandExecuted, CanRemoveDiagnosisCommandExecute);
            RemoveBlockCommand = new LambdaCommand(OnRemoveBlockCommandExecuted, CanRemoveBlockCommandExecute);
            RemoveLineCommand = new LambdaCommand(OnRemoveLineCommandExecuted, CanRemoveLineCommandExecute);

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

        #region DataCollection : ObservableCollection<Section> - Коллекция данных из базы данных

        /// <summary>Коллекция данных из базы данных</summary>
        private HashSet<Section> _DataCollection;

        /// <summary>Коллекция данных из базы данных</summary>
        public HashSet<Section> DataCollection
        {
            get => _DataCollection;
            set => Set(ref _DataCollection, value);
        }

        #endregion

        #region DiagnosisList : ObservableCollection<Section> - Коллекция кодов диагнозов

        /// <summary>Коллекция кодов диагнозов</summary>
        private ObservableCollection<Section> _DiagnosisList;

        /// <summary>Коллекция кодов диагнозов</summary>
        public ObservableCollection<Section> DiagnosisList
        {
            get => _DiagnosisList;
            set => Set(ref _DiagnosisList, value);
        }

        #endregion

        #region BlocksList : ObservableCollection<Section> - Коллекция названий блоков

        /// <summary>Коллекция названий блоков</summary>
        private ObservableCollection<Section> _BlocksList;

        /// <summary>Коллекция названий блоков</summary>
        public ObservableCollection<Section> BlocksList
        {
            get => _BlocksList;
            set => Set(ref _BlocksList, value);
        }

        #endregion

        #region LinesList : ObservableCollection<Section> - Коллекция содержимого строк

        /// <summary>Коллекция содержимого строк</summary>
        private ObservableCollection<Section> _LinesList;

        /// <summary>Коллекция содержимого строк</summary>
        public ObservableCollection<Section> LinesList
        {
            get => _LinesList;
            set => Set(ref _LinesList, value);
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
            //DataCollection = TestData.GetCollection();
            //DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);

            // Реальные данные
            DataCollection = _DataProviderService.LoadDataFromFile("file1");
            DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);

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
