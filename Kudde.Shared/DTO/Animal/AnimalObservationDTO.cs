using Kudde.Shared.DTO.Base;
namespace Kudde.Shared.DTO.Animal
{
    public class AnimalObservationDTO : BaseEntityDTO
    {
        public Guid AnimalObservationId { get; set; }
        public Guid AnimalId { get; set; }
        public DateTime ObservationDate { get; set; }
        public string? ConditionCode { get; set; }
        public decimal? NumericValue { get; set; }
        public string? TextValue { get; set; }
        public string? Flag { get; set; }
        public string? Notes { get; set; }
    }
}