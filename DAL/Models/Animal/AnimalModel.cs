using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Animal
{
    [Table("Animal")]
    public class AnimalModel : BaseEntity
    {
        [Key]
        public Guid AnimalId { get; set; }

        [Required, MaxLength(6)]
        public string AnimalTypeCode { get; set; }

        [Required, MaxLength(50)]
        public string DisplayIdentifier { get; set; }

        public int? BirthYear { get; set; }

        [Required, MaxLength(6)]
        public string Gender { get; set; }

        public Guid? MotherAnimalId { get; set; }
        public Guid? FatherAnimalId { get; set; }

        public bool IsBranded { get; set; }

        [MaxLength(500)]
        public string MedicalNote { get; set; }

        public int? TotalPregnancies { get; set; }
        public int? TotalCalves { get; set; }
        public DateTime? LastPregnancyDate { get; set; }

        // Navigation
        [ForeignKey("AnimalTypeCode")]
        public AnimalTypeModel AnimalType { get; set; }

        [ForeignKey("MotherAnimalId")]
        public AnimalModel Mother { get; set; }

        [ForeignKey("FatherAnimalId")]
        public AnimalModel Father { get; set; }
    }
}
