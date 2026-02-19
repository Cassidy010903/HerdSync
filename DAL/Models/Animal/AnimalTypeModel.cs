using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Animal
{
    [Table("AnimalType")]
    public class AnimalTypeModel : BaseEntity
    {
        [Key, MaxLength(6)]
        public string AnimalTypeCode { get; set; }

        [Required, MaxLength(50)]
        public string AnimalTypeName { get; set; }

        public string PlaceholderImage { get; set; }
    }
}