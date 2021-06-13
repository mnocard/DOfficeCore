using DOfficeCore.Data;
using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels.Core;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DOfficeCore.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(IDataProviderService DataProviderService, 
                                    IViewCollectionProvider ViewCollectionProvider, 
                                    IDiaryBoxProvider DiaryBoxProvider,
                                    ILineEditorService LineEditorService
                                    )
        {
            _DataProviderService = DataProviderService;
            _ViewCollectionProvider = ViewCollectionProvider;
            _DiaryBoxProvider = DiaryBoxProvider;
            _LineEditorService = LineEditorService;

            #region Команды окна дневника
            LoadDataCommand = new LambdaCommand(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
            ClosingAppCommand = new LambdaCommand(OnClosingAppCommandExecuted, CanClosingAppCommandExecute);

            SelectedDiagnosisCommand = new LambdaCommand(OnSelectedDiagnosisCommandExecuted, CanSelectedDiagnosisCommandExecute);
            SelectedBlockCommand = new LambdaCommand(OnSelectedBlockCommandExecuted, CanSelectedBlockCommandExecute);
            SelectedLineCommand = new LambdaCommand(OnSelectedLineCommandExecuted, CanSelectedLineCommandExecute);

            SearchElementCommand = new LambdaCommand(OnSearchElementCommandExecuted, CanSearchElementCommandExecute);
            
            RandomCommand = new LambdaCommand(OnRandomCommandExecuted, CanRandomCommandExecute);
            CopyTextCommand = new LambdaCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
            EditTextCommand = new LambdaCommand(OnEditTextCommandExecuted, CanEditTextCommandExecute);
            ClearDiaryBoxCommand = new LambdaCommand(OnClearDiaryBoxCommandExecuted, CanClearDiaryBoxCommandExecute);
            ChangeTabCommand = new LambdaCommand(OnChangeTabCommandExecuted, CanChangeTabCommandExecute);

            #endregion

            #region Команды окна редактирования строк
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            GetTextFromClipboardCommand = new LambdaCommand(OnGetTextFromClipboardCommandExecuted, CanGetTextFromClipboardCommandExecute); 
            SaveDataToFileCommand = new LambdaCommand(OnSaveDataToFileCommandExecuted, CanSaveDataToFileCommandExecute);
            ClearListBoxCommand = new LambdaCommand(OnClearListBoxCommandExecuted, CanClearListBoxCommandExecute);
            
            SelectedRawLineCommand = new LambdaCommand(OnSelectedRawLineCommandExecuted, CanSelectedRawLineCommandExecute);
            SelectedDiagnosisELCommand = new LambdaCommand(OnSelectedDiagnosisELCommandExecuted, CanSelectedDiagnosisELCommandExecute);
            SelectedBlockELCommand = new LambdaCommand(OnSelectedBlockELCommandExecuted, CanSelectedBlockELCommandExecute);
            SelectedLinesELCommand = new LambdaCommand(OnSelectedLinesELCommandExecuted, CanSelectedLinesELCommandExecute);

            AddDiagnosisCommand = new LambdaCommand(OnAddDiagnosisCommandExecuted, CanAddDiagnosisCommandExecute);
            AddBlockCommand = new LambdaCommand(OnAddBlockCommandExecuted, CanAddBlockCommandExecute);
            AddLineCommand = new LambdaCommand(OnAddLineCommandExecuted, CanAddLineCommandExecute);
            
            EditDiagnosisCommand = new LambdaCommand(OnEditDiagnosisCommandExecuted, CanEditDiagnosisCommandExecute);
            EditBlockCommand = new LambdaCommand(OnEditBlockCommandExecuted, CanEditBlockCommandExecute);
            EditLineCommand = new LambdaCommand(OnEditLineCommandExecuted, CanEditLineCommandExecute);

            RemoveDiagnosisCommand = new LambdaCommand(OnRemoveDiagnosisCommandExecuted, CanRemoveDiagnosisCommandExecute);
            RemoveBlockCommand = new LambdaCommand(OnRemoveBlockCommandExecuted, CanRemoveBlockCommandExecute);
            RemoveLineCommand = new LambdaCommand(OnRemoveLineCommandExecuted, CanRemoveLineCommandExecute);

            ReturnLineCommand = new LambdaCommand(OnReturnLineCommandExecuted, CanReturnLineCommandExecute);
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

        #region DiaryVisibility : bool - Видимость вкладки дневника

        /// <summary>Видимость вкладки дневника</summary>
        private Visibility _DiaryVisibility = Visibility.Visible;

        /// <summary>Видимость вкладки дневника</summary>
        public Visibility DiaryVisibility
        {
            get => _DiaryVisibility;
            set => Set(ref _DiaryVisibility, value);
        }

        #endregion

        #region LineEditorVisibility : bool - Видимость вкладки обработки предложений

        /// <summary>Видимость вкладки обработки предложений</summary>
        private Visibility _LineEditorVisibility = Visibility.Collapsed;

        /// <summary>Видимость вкладки обработки предложений</summary>
        public Visibility LineEditorVisibility
        {
            get => _LineEditorVisibility;
            set => Set(ref _LineEditorVisibility, value);
        }

        #endregion

        #region DiaryZIndex : int - Положение вкладки дневника

        /// <summary>Положение вкладки дневника</summary>
        private int _DiaryZIndex = 1;

        /// <summary>Положение вкладки дневника</summary>
        public int DiaryZIndex
        {
            get => _DiaryZIndex;
            set => Set(ref _DiaryZIndex, value);
        }

        #endregion

        #region LineEditorZIndex : int - Положение вкладки обработки предложений

        /// <summary>Положение вкладки обработки предложений</summary>
        private int _LineEditorZIndex = 0;

        /// <summary>Положение вкладки обработки предложений</summary>
        public int LineEditorZIndex
        {
            get => _LineEditorZIndex;
            set => Set(ref _LineEditorZIndex, value);
        }

        #endregion

        #region Status : string - Состояние программы

        /// <summary>Состояние программы</summary>
        private string _Status = "Привет :)";

        /// <summary>Состояние программы</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #region DataCollection : ObservableCollection<Section> - Коллекция данных из базы данных

        /// <summary>Коллекция данных из базы данных</summary>
        private List<Section> _DataCollection;

        /// <summary>Коллекция данных из базы данных</summary>
        public List<Section> DataCollection
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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder)
                .CreateLogger();

            try
            {
                Log.Information("INFO");

                // Тестовые данные
                DataCollection = TestData.GetCollection();
                DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);

                // Реальные данные
                //DataCollection = _DataProviderService.LoadDataFromFile("lines");
                //DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
            }
            catch (Exception e)
            {
                Log.Error($"Unexpected error\n{e.Message}");
                MessageBox.Show("Ошибка загрузки данных. Данные не загружены.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Log.Information("DONE");
        }

        private bool CanLoadDataCommandExecute(object parameter) => true;
        #endregion
        
        #region Команда закрытия программы
        /// <summary>Команда закрытия программы</summary>
        public ICommand ClosingAppCommand { get; }
        /// <summary>Команда закрытия программы</summary>
        private void OnClosingAppCommandExecuted(object parameter) => Log.CloseAndFlush();

        private bool CanClosingAppCommandExecute(object parameter) => true;

        #endregion


        #region Команда переключения вкладок между дневником и обработкой предложений
        /// <summary>Команда переключения вкладок между дневником и обработкой предложений</summary>
        public ICommand ChangeTabCommand { get; }
        /// <summary>Команда переключения вкладок между дневником и обработкой предложений</summary>
        private void OnChangeTabCommandExecuted(object parameter)
        {
            if (DiaryVisibility is Visibility.Visible && LineEditorVisibility is Visibility.Collapsed && DiaryZIndex > LineEditorZIndex)
            {
                DiaryVisibility = Visibility.Collapsed;
                LineEditorVisibility = Visibility.Visible;
                DiaryZIndex = 0;
                LineEditorZIndex = 1;
            }
            else if (DiaryVisibility is Visibility.Collapsed && LineEditorVisibility is Visibility.Visible && DiaryZIndex < LineEditorZIndex)
            {
                DiaryVisibility = Visibility.Visible;
                LineEditorVisibility = Visibility.Collapsed;
                DiaryZIndex = 1;
                LineEditorZIndex = 0;
            }
            else
            {
                MessageBox.Show("Непредвиденная ошибка! Не удаётся переключиться между окнами.", "Ошибка!", MessageBoxButton.OK);
            }
        }

        private bool CanChangeTabCommandExecute(object parameter) => true;

        #endregion

        #endregion

        #region Сервисы

        #region Сервис обработки строк
        private readonly ILineEditorService _LineEditorService;
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
