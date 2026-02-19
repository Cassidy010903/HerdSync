using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramRunAnimalDTO : BaseEntityDTO
    {
        public Guid ProgramRunAnimalId { get; set; }
        public Guid ProgramRunId { get; set; }
        public Guid AnimalId { get; set; }
        public bool WasHandled { get; set; }
        public string SkippedReason { get; set; }
    }
}