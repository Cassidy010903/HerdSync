using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Treatment
{
    public class TreatmentProductDTO : BaseEntityDTO
    {
        public Guid TreatmentProductId { get; set; }
        public string TreatmentCode { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public string DosageInstructions { get; set; }
    }
}