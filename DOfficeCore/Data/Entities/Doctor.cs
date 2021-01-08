using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DOfficeCore.Data.Entities.Core;

namespace DOfficeCore.Data.Entities
{
    public class Doctor : Entity
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public virtual Position MainPosition { get; set; }
        public virtual ICollection<Position> SubPosition { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}