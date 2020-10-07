using DOfficeCore.Infrastructure.Commands;
using DOfficeCore.Models;
using DOfficeCore.Services;
using DOfficeCore.Services.Interfaces;
using DOfficeCore.ViewModels.Core;
using System;
using System.Collections.ObjectModel;

namespace DOfficeCore.ViewModels
{
    internal partial class MainWindowViewModel : ViewModelCore
    {
        public MainWindowViewModel(IDataProviderService DataProviderService, 
                                    IViewCollectionProvider ViewCollectionProvider, 
                                    IViewCollection ViewCollection,
                                    IDiaryBoxProvider DiaryBoxProvider)
        {
            _DataProviderService = DataProviderService;
            _ViewCollectionProvider = ViewCollectionProvider;
            _ViewCollection = ViewCollection;
            _DiaryBoxProvider = DiaryBoxProvider;

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

            #endregion
        }

        #region Свойства

        #region Сервис работы с файлами
        private readonly IDataProviderService _DataProviderService;
        #endregion

        #region Сервис работы с данными
        private readonly IViewCollectionProvider _ViewCollectionProvider;
        #endregion

        #region Сервис работы с дневником
        private readonly IDiaryBoxProvider _DiaryBoxProvider;
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
        
        #region ViewCollection : IViewCollection - Коллекция данных
        private readonly IViewCollection _ViewCollection;
        public IViewCollection ViewCollection { get => _ViewCollection; }
        #endregion

        #region EnableTextBox : bool - Состояние возможности редактирования текстового окна
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        private bool _EnableTextBox = true;
        /// <summary>Состояние возможности редактирования текстового окна</summary>
        public bool EnableTextBox
        {
            get => _EnableTextBox;
            set => Set(ref _EnableTextBox, value);
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

        #region ChoosenDate : Datetime - Выбранная дата

        /// <summary>Выбранная дата</summary>
        private DateTime _ChoosenDate = DateTime.Now;

        /// <summary>Выбранная дата</summary>
        public DateTime ChoosenDate
        {
            get => _ChoosenDate;
            set => Set(ref _ChoosenDate, value);
        }

        #endregion

        #region Doctors : ObservableCollection<string> - Список докторов

        /// <summary>Список докторов</summary>
        private ObservableCollection<string> _Doctors;

        /// <summary>Список докторов</summary>
        public ObservableCollection<string> Doctors
        {
            get => _Doctors;
            set => Set(ref _Doctors, value);
        }

        #endregion

        #region Position : ObservableCollection<string> - Должность

        /// <summary>DESCRIPTION</summary>
        private ObservableCollection<string> _Position;

        /// <summary>DESCRIPTION</summary>
        public ObservableCollection<string> Position
        {
            get => _Position;
            set => Set(ref _Position, value);
        }

        #endregion

        #region CurrentPosition : string - Поле ввода для должностей

        /// <summary>Поле ввода для должностей</summary>
        private string _CurrentPosition;

        /// <summary>Поле ввода для должностей</summary>
        public string CurrentPosition
        {
            get => _CurrentPosition;
            set => Set(ref _CurrentPosition, value);
        }

        #endregion

        #region CurrentDoctor : string - Поле ввода для докторов

        /// <summary>Поле ввода для докторов</summary>
        private string _CurrentDoctor;

        /// <summary>Поле ввода для докторов</summary>
        public string CurrentDoctor
        {
            get => _CurrentDoctor;
            set => Set(ref _CurrentDoctor, value);
        }

        #endregion

        #endregion
    }
}
