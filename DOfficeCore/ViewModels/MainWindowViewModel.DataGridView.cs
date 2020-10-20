using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        #region Команды вкладки дневника

        #region Создание случайного дневника
        /// <summary>Создание случайного дневника</summary>
        public ICommand RandomCommand { get; }
        /// <summary>Создание случайного дневника</summary>
        private void OnRandomCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (_ViewCollection.CurrentDiagnosis != null)
            {
                DiaryBox = _ViewCollectionProvider.RandomDiary();
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanRandomCommandExecute(object parameter) => true;

        #endregion

        #region Команда добавления подписи в дневник
        /// <summary>Команда добавления подписи в дневник</summary>
        public ICommand AddDocToDiaryCommand { get; }
        /// <summary>Команда добавления подписи в дневник</summary>
        private void OnAddDocToDiaryCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter is object[] sign && sign[0] is string position && sign[1] is string doctor)
            {
                DiaryBox = _DiaryBoxProvider.DocToDiary(DiaryBox, position, doctor);
            }
            _Logger.WriteLog("DONE");
        }

        private bool CanAddDocToDiaryCommandExecute(object parameter) => true;

        #endregion

        #region Команда сохранения списка должностей и врачей
        /// <summary>Команда сохранения списка должностей и врачей</summary>
        public ICommand SaveDoctorsListCommand { get; }
        /// <summary>Команда сохранения списка должностей и врачей</summary>
        private void OnSaveDoctorsListCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (Doctors != null)
                _DataProviderService.SaveDataToFile(Doctors, "Doctors");
            if (Position != null)
                _DataProviderService.SaveDataToFile(Position, "Position");

            _Logger.WriteLog("DONE");
        }

        private bool CanSaveDoctorsListCommandExecute(object parameter) => true;

        #endregion

        #region Команда редактирования выбранной в комбобоксе должности
        /// <summary>Команда редактирования выбранной в комбобоксе должности</summary>
        public ICommand EditPositionCommand { get; }
        /// <summary>Команда редактирования выбранной в комбобоксе должности</summary>
        private void OnEditPositionCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter is string positionName
                && Position.Contains(positionName)
                && CurrentPosition != null
                && CurrentPosition != String.Empty)
            {
                Position.Remove(positionName);
                Position.Add(CurrentPosition);
                CurrentPosition = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanEditPositionCommandExecute(object parameter) => true;

        #endregion

        #region Команда удаления должности
        /// <summary>Команда удаления должности</summary>
        public ICommand DeletePositionCommand { get; }
        /// <summary>Команда удаления должности</summary>
        private void OnDeletePositionCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter is string positionName && Position.Contains(positionName))
            {
                Position.Remove(positionName);
                CurrentPosition = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanDeletePositionCommandExecute(object parameter) => true;

        #endregion

        #region Команда добавление должности
        /// <summary>Команда добавление должности</summary>
        public ICommand AddPositionCommand { get; }
        /// <summary>Команда добавление должности</summary>
        private void OnAddPositionCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (Position == null) Position = new ObservableCollection<string>();
            if (CurrentPosition != null && CurrentPosition != String.Empty)
            {
                Position.Add(CurrentPosition);
                CurrentPosition = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanAddPositionCommandExecute(object parameter) => true;

        #endregion

        #region Команда переименования доктора
        /// <summary>Команда переименования доктора</summary>
        public ICommand EditDoctorCommand { get; }
        /// <summary>Команда переименования доктора</summary>
        private void OnEditDoctorCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter is string doctorsName
                && Doctors.Contains(doctorsName)
                && CurrentDoctor != null
                && CurrentDoctor != String.Empty)
            {
                Doctors.Remove(doctorsName);
                Doctors.Add(CurrentDoctor);
                CurrentDoctor = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanEditDoctorCommandExecute(object parameter) => true;

        #endregion

        #region Команда удаления из списка выбранного в комбобоксе доктора
        /// <summary>Команда удаления из списка выбранного в комбобоксе доктора</summary>
        public ICommand DeleteDoctorCommand { get; }
        /// <summary>Команда удаления из списка выбранного в комбобоксе доктора</summary>
        private void OnDeleteDoctorCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter is string doctorsName && Doctors.Contains(doctorsName))
            {
                Doctors.Remove(doctorsName);
                CurrentDoctor = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanDeleteDoctorCommandExecute(object parameter) => true;

        #endregion

        #region Команда добавления нового доктора в список
        /// <summary>Команда добавления нового доктора в список</summary>
        public ICommand AddDoctorCommand { get; }
        /// <summary>Команда добавления нового доктора в список</summary>
        private void OnAddDoctorCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (Doctors == null) Doctors = new ObservableCollection<string>();
            if (CurrentDoctor != null && CurrentDoctor != String.Empty)
            {
                Doctors.Add(CurrentDoctor);
                CurrentDoctor = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanAddDoctorCommandExecute(object parameter) => true;

        #endregion

        #region Команда редактирования текста в окне дневника
        /// <summary>Команда редактирования текста в окне дневника</summary>
        public ICommand EditTextCommand { get; }
        /// <summary>Команда редактирования текста в окне дневника</summary>
        private void OnEditTextCommandExecuted(object p)
        {
            _Logger.WriteLog("INFO");

            if (EnableDiaryBox) EnableDiaryBox = false;

            _Logger.WriteLog("DONE");
        }

        private bool CanEditTextCommandExecute(object p) => EnableDiaryBox;
        #endregion

        #region Команда копирования текста
        /// <summary>Команда копирования текста</summary>
        public ICommand CopyTextCommand { get; }
        /// <summary>Команда копирования текста</summary>
        private void OnCopyTextCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter is string temp && temp != string.Empty && temp != "")
            {
                Clipboard.SetText(temp);
                EnableDiaryBox = true;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanCopyTextCommandExecute(object parameter) => parameter is string temp && temp != string.Empty && temp != "";
        #endregion

        #region Команда сохранения данных в файл
        /// <summary>Команда сохранения данных в файл</summary>
        public ICommand SaveDataToFileCommand { get; }
        /// <summary>Команда сохранения данных в файл</summary>
        private void OnSaveDataToFileCommandExecuted(object p)
        {
            _Logger.WriteLog("INFO");

            if (_ViewCollection.DataCollection != null)
                _DataProviderService.SaveDataToFile(_ViewCollection.DataCollection, "file1");

            _Logger.WriteLog("DONE");
        }

        private bool CanSaveDataToFileCommandExecute(object p) => true;
        #endregion

        #region Команда изменения отображения данных по щелчку
        /// <summary>Команда изменения отображения данных по щелчку</summary>
        public ICommand SelectedDataCommand { get; }
        /// <summary>Команда изменения отображения данных по щелчку</summary>
        private void OnSelectedDataCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if ((parameter is DataGrid datagrid) && datagrid.CurrentItem != null)
            {
                FocusedDataGrid = datagrid.Name;
                _ViewCollectionProvider.SelectedData(FocusedDataGrid, (string)datagrid.CurrentItem);
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanSelectedDataCommandExecute(object parameter) => parameter != null;

        #endregion

        #region Команда редактирования выбранного элемента
        /// <summary>Команда редактирования выбранного элемента</summary>
        public ICommand EditElementCommand { get; }
        /// <summary>Команда редактирования выбранного элемента</summary>
        private void OnEditElementCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (FocusedDataGrid != null && MultiBox.Length > 3) _ViewCollectionProvider.EditElement(FocusedDataGrid, MultiBox);

            _Logger.WriteLog("DONE");
        }

        private bool CanEditElementCommandExecute(object parameter) => true;

        #endregion

        #region Команда отправки элемента из датагрид в мультибокс
        /// <summary>Команда отправки элемента из датагрид в мультибокс</summary>
        public ICommand StringTransferCommand { get; }
        /// <summary>Команда отправки элемента из датагрид в мультибокс</summary>
        private void OnStringTransferCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if ((parameter is DataGrid datagrid) && datagrid.CurrentItem != null)
            {
                MultiBox = (string)datagrid.CurrentItem;
                FocusedDataGrid = datagrid.Name;
                if (FocusedDataGrid.Equals("dgLinesContent"))
                {
                    DiaryBox = _DiaryBoxProvider.LineToDiaryBox(DiaryBox, MultiBox);
                }
                EnableDiaryBox = true;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanStringTransferCommandExecute(object parameter) => true;

        #endregion

        #region Команда поиска элементов
        /// <summary>Команда поиска элементов</summary>
        public ICommand SearchElementCommand { get; }
        /// <summary>Команда поиска элементов</summary>
        private void OnSearchElementCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (MultiBox != null && MultiBox != string.Empty && MultiBox.Length > 3)
            {
                _ViewCollectionProvider.SearchElement(MultiBox);
                FocusedDataGrid = null;
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanSearchElementCommandExecute(object parameter) => true;

        #endregion

        #region Команда удаления элементов из списка
        /// <summary>Команда удаления элементов из списка</summary>
        public ICommand RemoveElementCommand { get; }
        /// <summary>Команда удаления элементов из списка</summary>
        private void OnRemoveElementCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (FocusedDataGrid != null && MultiBox != null) _ViewCollectionProvider.RemoveElement(FocusedDataGrid, MultiBox);

            _Logger.WriteLog("DONE");
        }

        private bool CanRemoveElementCommandExecute(object parameter) => true;

        #endregion

        #region Команда добавления элемента
        /// <summary>Команда добавления элемента</summary>
        public ICommand AddElementCommand { get; }
        /// <summary>Команда добавления элемента</summary>
        private void OnAddElementCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (parameter != null && MultiBox != null)
            {
                FocusedDataGrid = parameter as string;
                _ViewCollectionProvider.AddELement(FocusedDataGrid, MultiBox);
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanAddElementCommandExecute(object parameter) => true;


        #endregion

        #region Команда добавления времени в дневник
        /// <summary>Команда добавления времени в дневник</summary>
        public ICommand AddTimeCommand { get; }
        /// <summary>Команда добавления времени в дневник</summary>
        private void OnAddTimeCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            DiaryBox = _DiaryBoxProvider.TimeToDiaryBox(DiaryBox, ChoosenDate);

            _Logger.WriteLog("DONE");
        }

        private bool CanAddTimeCommandExecute(object parameter) => true;

        #endregion

        #region Команда добавления даты
        /// <summary>Команда добавления даты</summary>
        public ICommand AddDateCommand { get; }
        /// <summary>Команда добавления даты</summary>
        private void OnAddDateCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            DiaryBox = _DiaryBoxProvider.DateToDiaryBox(DiaryBox, ChoosenDate);

            _Logger.WriteLog("DONE");
        }

        private bool CanAddDateCommandExecute(object parameter) => true;


        #endregion

        #region Команда очистки дневника
        /// <summary>Команда очистки дневника</summary>
        public ICommand ClearDiaryBoxCommand { get; }
        /// <summary>Команда очистки дневника</summary>
        private void OnClearDiaryBoxCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            DiaryBox = null;
            EnableDiaryBox = true;

            _Logger.WriteLog("DONE");
        }

        private bool CanClearDiaryBoxCommandExecute(object parameter) => true;

        #endregion

        #endregion
    }
}
