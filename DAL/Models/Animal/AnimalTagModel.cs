using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Animal
{
    [Table("AnimalTag")]
    public class AnimalTagModel : BaseEntity
    {
        [Key]
        public Guid AnimalTagId { get; set; }

        [Required, MaxLength(50)]
        public string RFIDTagCode { get; set; }

        public Guid AnimalId { get; set; }

        public DateTime AssignedDate { get; set; }
        public DateTime? UnassignedDate { get; set; }

        public bool IsCurrent { get; set; }

        // Navigation
        [ForeignKey("AnimalId")]
        public AnimalModel Animal { get; set; }
    }
}