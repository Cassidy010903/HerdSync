using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Farm
{
    public class FarmActivityDTO : BaseEntityDTO
    {
        public Guid FarmActivityId { get; set; }
        public Guid AnimalId { get; set; }
        public string FarmActivityTypeCode { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Notes { get; set; }
        public int? Quantity { get; set; }
        public string ReferenceNumber { get; set; }
    }
}