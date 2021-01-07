using System.ComponentModel.DataAnnotations;

namespace DOfficeCore.Data.Entities.Core
{
    public abstract class Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
