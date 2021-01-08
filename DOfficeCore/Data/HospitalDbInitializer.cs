using System;
using System.Linq;
using DOfficeCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOfficeCore.Data
{
    class HospitalDbInitializer
    {
        private readonly HospitalDb _db;
        public HospitalDbInitializer(HospitalDb db)
        {
            _db = db;
        }
        public void Initialize()
        {
            _db.Database.Migrate();
            InitDepartments();
            InitDoctors();
            InitPatients();
            InitPositions();
        }

        private void InitPatients()
        {
            if (_db.Patients.Any()) return;
            for (int i = 0; i < 100; i++)
            {
                var patient = new Patient
                {
                    Name = $"Name {i}",
                    Surname = $"Surname {i}",
                    Patronymic = $"Patronymic {i}",
                    BirthDay = DateTime.Now,
                    ReceiptDate = DateTime.Now,
                    Diagnosis = $"F{i}"
                };
                _db.Patients.Add(patient);
            }
            _db.SaveChanges();
        }

        private void InitPositions()
        {
            if (_db.Positions.Any()) return;

            for (int i = 0; i < 10; i++)
            {
                var pos = new Position
                {
                    Name = $"Name {i}"
                };
                _db.Positions.Add(pos);
            }
            _db.SaveChanges();
        }

        private void InitDoctors()
        {
            if (_db.Doctors.Any()) return;

            for (int i = 0; i < 5; i++)
            {
                var doc = new Doctor
                {
                    Name = $"Doctor's Name {i}",
                    Surname = $"Doctor's Surname {i}"
                };
                _db.Doctors.Add(doc);
            }
            _db.SaveChanges();
        }

        private void InitDepartments()
        {
            if (_db.Departments.Any()) return;

            for (int i = 0; i < 5; i++)
            {
                var dep = new Department
                {
                    Name = $"Department {i}"
                };
                _db.Departments.Add(dep);
            }
            _db.SaveChanges();
        }
    }
}
