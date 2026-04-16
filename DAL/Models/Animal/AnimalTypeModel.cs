using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Animal
{
    [Table("AnimalType")]
    public class AnimalTypeModel
    {
        [Key, MaxLength(6)]
        public string AnimalTypeCode { get; set; }

        [Required, MaxLength(50)]
        public string AnimalTypeName { get; set; }

        public string? PlaceholderImage { get; set; }

        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}