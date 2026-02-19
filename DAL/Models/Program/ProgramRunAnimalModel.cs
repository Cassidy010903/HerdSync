using DAL.Models.Animal;
using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program
{
    [Table("ProgramRunAnimal")]
    public class ProgramRunAnimalModel : BaseEntity
    {
        [Key]
        public Guid ProgramRunAnimalId { get; set; }

        public Guid ProgramRunId { get; set; }
        public Guid AnimalId { get; set; }

        public bool WasHandled { get; set; }

        [MaxLength(200)]
        public string SkippedReason { get; set; }

        // Navigation
        [ForeignKey("ProgramRunId")]
        public ProgramRun ProgramRun { get; set; }

        [ForeignKey("AnimalId")]
        public AnimalModel Animal { get; set; }
    }
}