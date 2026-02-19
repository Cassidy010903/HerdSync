using DAL.Models.Animal;
using DAL.Models.Base;
using HerdSync.Shared.DTO.Program;
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
        public ProgramRunModel ProgramRun { get; set; }

        [ForeignKey("AnimalId")]
        public AnimalModel Animal { get; set; }
    }
}