using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Animal
{
    [Table("Pregnancy")]
    public class PregnancyModel : BaseEntity
    {
        [Key]
        public Guid PregnancyId { get; set; }

        public Guid MotherAnimalId { get; set; }
        public Guid? FatherAnimalId { get; set; }
        public Guid? CalfAnimalId { get; set; }

        public DateTime? ConceptionDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ExpectedBirthDate { get; set; }
        public DateTime? ActualBirthDate { get; set; }

        [MaxLength(20)]
        public string Outcome { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Navigation
        [ForeignKey("MotherAnimalId")]
        public AnimalModel Mother { get; set; }

        [ForeignKey("FatherAnimalId")]
        public AnimalModel Father { get; set; }

        [ForeignKey("CalfAnimalId")]
        public AnimalModel Calf { get; set; }
    }
}