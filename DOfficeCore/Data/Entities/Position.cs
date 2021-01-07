using System.Collections;
using System.Collections.Generic;
using DOfficeCore.Data.Entities.Core;

namespace DOfficeCore.Data.Entities
{
    public class Position : Entity
    {
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}