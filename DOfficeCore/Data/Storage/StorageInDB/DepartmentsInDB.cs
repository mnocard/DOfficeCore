using DOfficeCore.Data.Entities;

namespace DOfficeCore.Data.Storage.StorageInDB
{
    public class DepartmentsInDB : StorageInDB<Department>
    {
        public DepartmentsInDB(HospitalDb db) : base (db) { }
    }

    public class DoctorsInDB : StorageInDB<Doctor>
    {
        public DoctorsInDB(HospitalDb db) : base(db) { }
    }
    public class PatientsInDB : StorageInDB<Patient>
    {
        public PatientsInDB(HospitalDb db) : base(db) { }
    }
    public class PositionsInDB : StorageInDB<Position>
    {
        public PositionsInDB(HospitalDb db) : base(db) { }
    }
}
