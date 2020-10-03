using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.ViewModels.Core;

namespace DOfficeCore.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(IDataProviderService DataProviderService, IViewCollectionProvider ViewCollectionProvider)
        {
            _DataProviderService = DataProviderService;
            _ViewCollectionProvider = ViewCollectionProvider;
            CurrentData = new ViewCollection();

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

            #endregion
        }

        #region Свойства

        #region Сервис работы с файлами
        private readonly IDataProviderService _DataProviderService;
        #endregion

        #region Сервис работы с данными
        private readonly IViewCollectionProvider _ViewCollectionProvider;
        #endregion

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

        #region Состояние возможности редактирования текстового окна
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        private bool _EnableTextBox = true;
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        public bool EnableTextBox
        {
            get => _EnableTextBox;
            set => Set(ref _EnableTextBox, value);
        }
        #endregion

        #region Активная коллекция данных
        /// <summary>Коллекция данных для отправки в дерево</summary>
        private ViewCollection _CurrentData;
        /// <summary>Коллекция данных для отправки в дерево</summary>
        public ViewCollection CurrentData
        {
            get => _CurrentData;
            set => Set(ref _CurrentData, value);
        }
        #endregion

        #region FocusedDataGrid : DataGrid - Имя датагрида, который сейчас находится в фокусе

        /// <summary>Имя датагрида, который сейчас находится в фокусе</summary>
        private string _FocusedDataGrid;

        /// <summary>Имя датагрида, который сейчас находится в фокусе</summary>
        public string FocusedDataGrid
        {
            get => _FocusedDataGrid;
            set => Set(ref _FocusedDataGrid, value);
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

        #endregion

        
    }
}
