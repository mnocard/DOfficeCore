using System;
using System.ComponentModel.DataAnnotations;
using DOfficeCore.Data.Entities.Core;

namespace DOfficeCore.Data.Entities
{
    public class Patient : Entity
    {
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public virtual Department Department { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string Diagnosis { get; set; }
    }
}
