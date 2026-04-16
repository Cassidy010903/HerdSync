using Kudde.Shared.DTO.Animal;
using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Program
{
    public class ProgramRunAnimalDTO : BaseEntityDTO
    {
        public Guid ProgramRunAnimalId { get; set; }
        public Guid ProgramRunId { get; set; }
        public Guid AnimalId { get; set; }
        public AnimalDTO? Animal { get; set; }
        public bool WasHandled { get; set; }
        public string? SkippedReason { get; set; }
    }
}