using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DOfficeCore.Data.Entities;

namespace DOfficeCore.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Свойства

        #region Departments : ObservableCollection<Department> - Список отделении

        /// <summary>Список отделении</summary>
        private ObservableCollection<Department> _Departments;

        /// <summary>Список отделении</summary>
        public ObservableCollection<Department> DepartmentsDB
        {
            get => _Departments;
            set => Set(ref _Departments, value);
        }
        #endregion

        #region Doctors : ObservableCollection<Doctor> - Список докторов

        /// <summary>Список докторов</summary>
        private ObservableCollection<Doctor> _DoctorsDB;

        /// <summary>Список докторов</summary>
        public ObservableCollection<Doctor> DoctorsDB
        {
            get => _DoctorsDB;
            set => Set(ref _DoctorsDB, value);
        }

        #endregion

        #region PositionsDB : ObservableCollection<string> - Список должностей

        /// <summary>Список должностей</summary>
        private ObservableCollection<Position> _PositionsDB;

        /// <summary>Список должностей</summary>
        public ObservableCollection<Position> PositionsDB
        {
            get => _PositionsDB;
            set => Set(ref _PositionsDB, value);
        }

        #endregion

        #region Patients : ObservableCollection<Patient> - Список пациентов

        /// <summary>Список пациентов</summary>
        private ObservableCollection<Patient> _Patients;

        /// <summary>Список пациентов</summary>
        public ObservableCollection<Patient> PatientsDB
        {
            get => _Patients;
            set => Set(ref _Patients, value);
        }

        #endregion
        #endregion

        #region Команды

        #region Просмотр списка докторов
        /// <summary>Просмотр списка докторов</summary>
        public ICommand ShowDoctorsCommand { get; }
        /// <summary>Просмотр списка докторов</summary>
        private void OnShowDoctorsCommandExecuted(object parameter)
        {
            if (parameter is TreeView treeView)
            {
                treeView.ItemsSource = DoctorsDB;
            }
        }

        private bool CanShowDoctorsCommandExecute(object parameter) => true;

        #endregion

        #region Просмотр списка пациентов
        /// <summary>Просмотр списка пациентов</summary>
        public ICommand ShowPatientsCommand { get; }
        /// <summary>Просмотр списка пациентов</summary>
        private void OnShowPatientsCommandExecuted(object parameter)
        {
            if (parameter is TreeView treeView)
            {
                treeView.ItemsSource = PatientsDB;
            }
        }

        private bool CanShowPatientsCommandExecute(object parameter) => true;

        #endregion

        #region Просмотр списка отделений
        /// <summary>Просмотр списка отделений</summary>
        public ICommand ShowDepartmentsCommand { get; }
        /// <summary>Просмотр списка отделений</summary>
        private void OnShowDepartmentsCommandExecuted(object parameter)
        {
            if (parameter is TreeView treeView)
            {
                treeView.ItemsSource = DepartmentsDB;
            }
        }

        private bool CanShowDepartmentsCommandExecute(object parameter) => true;

        #endregion

        #endregion
    }
}
