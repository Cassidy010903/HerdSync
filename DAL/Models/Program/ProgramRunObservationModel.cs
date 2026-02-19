using DAL.Models.Base;
using DAL.Models.Treatment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program
{
    [Table("ProgramRunObservation")]
    public class ProgramRunObservationModel : BaseEntity
    {
        [Key]
        public Guid ProgramRunObservationId { get; set; }

        public Guid ProgramRunAnimalId { get; set; }

        [MaxLength(10)]
        public string ConditionCode { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? NumericValue { get; set; }

        [MaxLength(300)]
        public string TextValue { get; set; }

        [MaxLength(50)]
        public string Flag { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Navigation
        [ForeignKey("ProgramRunAnimalId")]
        public ProgramRunAnimalModel ProgramRunAnimal { get; set; }

        [ForeignKey("ConditionCode")]
        public ConditionModel Condition { get; set; }
    }
}