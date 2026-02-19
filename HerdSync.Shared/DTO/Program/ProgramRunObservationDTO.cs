using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramRunObservationDTO : BaseEntityDTO
    {
        public Guid ProgramRunObservationId { get; set; }
        public Guid ProgramRunAnimalId { get; set; }
        public string ConditionCode { get; set; }
        public decimal? NumericValue { get; set; }
        public string TextValue { get; set; }
        public string Flag { get; set; }
        public string Notes { get; set; }
    }
}