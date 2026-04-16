using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Animal
{
    public class AnimalTagDTO : BaseEntityDTO
    {
        public Guid AnimalTagId { get; set; }
        public string RFIDTagCode { get; set; }
        public Guid AnimalId { get; set; }
        public AnimalDTO Animal { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? UnassignedDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}