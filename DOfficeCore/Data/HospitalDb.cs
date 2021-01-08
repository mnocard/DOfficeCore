using System;
using System.Collections.Generic;
using System.Text;
using DOfficeCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOfficeCore.Data
{
    public class HospitalDb : DbContext
    {
        public HospitalDb(DbContextOptions<HospitalDb> db) : base (db) { }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
