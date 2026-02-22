using HerdSync.Shared.DTO.Animal;
using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramRunTreatmentDTO : BaseEntityDTO
    {
        public Guid ProgramRunTreatmentId { get; set; }
        public Guid ProgramRunAnimalId { get; set; }
        public AnimalDTO Animal { get; set; }
        public string TreatmentCode { get; set; }
        public bool IsAutoApplied { get; set; }
        public bool AddedManually { get; set; }
        public DateTime GivenDate { get; set; }
    }
}