using System.Collections.Generic;
using DOfficeCore.Data.Entities.Core;

namespace DOfficeCore.Data.Entities
{
    public class Department : Entity
    {
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}