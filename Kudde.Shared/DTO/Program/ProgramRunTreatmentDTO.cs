using Kudde.Shared.DTO.Animal;
using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Program
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