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
            _Logger.WriteLog("INFO");

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
            _Logger.WriteLog("DONE");
        }

        private bool CanSelectedDataCommandExecute(object parameter) => parameter != null;

        #endregion

        #region Добавление даты в дневник
        /// <summary>Добавление даты в дневник</summary>
        public ICommand AddDateCommand { get; }
        /// <summary>Добавление даты в дневник</summary>
        private void OnAddDateCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            DiaryBox = _DiaryBoxProvider.DateToDiaryBox(DiaryBox, ChoosenDate);

            _Logger.WriteLog("DONE");
        }

        private bool CanAddDateCommandExecute(object parameter) => true;


        #endregion

        #region Добавление времени в дневник
        /// <summary>Добавление времени в дневник</summary>
        public ICommand AddTimeCommand { get; }
        /// <summary>Добавление времени в дневник</summary>
        private void OnAddTimeCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            DiaryBox = _DiaryBoxProvider.TimeToDiaryBox(DiaryBox, ChoosenDate);

            _Logger.WriteLog("DONE");
        }

        private bool CanAddTimeCommandExecute(object parameter) => true;

        #endregion

        #region Добавление подписи в дневник
        /// <summary>Добавление подписи в дневник</summary>
        public ICommand AddDocToDiaryCommand { get; }
        /// <summary>Добавление подписи в дневник</summary>
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

        #region Добавление нового доктора в список докторов
        /// <summary>Добавление нового доктора в список докторов</summary>
        public ICommand AddDoctorCommand { get; }
        /// <summary>Добавление нового доктора в список докторов</summary>
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

        #region Переименование доктора
        /// <summary>Переименование доктора</summary>
        public ICommand EditDoctorCommand { get; }
        /// <summary>Переименование доктора</summary>
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

        #region Удаление из списка выбранного в комбобоксе доктора
        /// <summary>Удаление из списка выбранного в комбобоксе доктора</summary>
        public ICommand DeleteDoctorCommand { get; }
        /// <summary>Удаление из списка выбранного в комбобоксе доктора</summary>
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

        #region Добавление должности в список должностей
        /// <summary>Добавление должности в список должностей</summary>
        public ICommand AddPositionCommand { get; }
        /// <summary>Добавление должности в список должностей</summary>
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

        #region Редактирование выбранной в комбобоксе должности
        /// <summary>Редактирование выбранной в комбобоксе должности</summary>
        public ICommand EditPositionCommand { get; }
        /// <summary>Редактирование выбранной в комбобоксе должности</summary>
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

        #region Удаление из списка выбранной в комбобоксе должности
        /// <summary>Удаление из списка выбранной в комбобоксе должности</summary>
        public ICommand DeletePositionCommand { get; }
        /// <summary>Удаление из списка выбранной в комбобоксе должности</summary>
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

        #region Сохранение списка должностей и врачей
        /// <summary>Сохранение списка должностей и врачей</summary>
        public ICommand SaveDoctorsListCommand { get; }
        /// <summary>Сохранение списка должностей и врачей</summary>
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

        #region Команда поиска элементов
        /// <summary>Команда поиска элементов</summary>
        public ICommand SearchElementCommand { get; }
        /// <summary>Команда поиска элементов</summary>
        private void OnSearchElementCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");

            if (MultiBox != null && MultiBox != string.Empty && MultiBox.Length >= 3)
            {
                DiagnosisList = _ViewCollectionProvider.SearchDiagnosis(DataCollection, MultiBox);
                if (DiagnosisList.Count == 0) DiagnosisList = _ViewCollectionProvider.DiagnosisFromDataToView(DataCollection);
                BlocksList = _ViewCollectionProvider.SearchBlocks(DataCollection, MultiBox);
                LinesList = _ViewCollectionProvider.SearchLines(DataCollection, MultiBox);
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanSearchElementCommandExecute(object parameter) => true;

        #endregion

        #region Создание случайного дневника
        /// <summary>Создание случайного дневника</summary>
        public ICommand RandomCommand { get; }
        /// <summary>Создание случайного дневника</summary>
        private void OnRandomCommandExecuted(object parameter)
        {
            _Logger.WriteLog("INFO");
            if ((parameter is ListBox) && CurrentSection != null)
            {
                DiaryBox = _ViewCollectionProvider.RandomDiary(DataCollection, CurrentSection);
            }

            _Logger.WriteLog("DONE");
        }

        private bool CanRandomCommandExecute(object parameter) => true;

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
