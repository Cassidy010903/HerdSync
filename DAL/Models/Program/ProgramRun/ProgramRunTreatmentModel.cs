using DAL.Models.Base;
using DAL.Models.Treatment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program.ProgramRun
{
    [Table("ProgramRunTreatment")]
    public class ProgramRunTreatmentModel : BaseEntity
    {
        [Key]
        public Guid ProgramRunTreatmentId { get; set; }

        public Guid ProgramRunAnimalId { get; set; }

        [Required, MaxLength(10)]
        public string TreatmentCode { get; set; }

        public bool IsAutoApplied { get; set; }
        public bool AddedManually { get; set; }
        public DateTime GivenDate { get; set; }

        // Navigation
        [ForeignKey("ProgramRunAnimalId")]
        public ProgramRunAnimalModel ProgramRunAnimal { get; set; }

        [ForeignKey("TreatmentCode")]
        public TreatmentModel Treatment { get; set; }
    }
}