using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Treatment
{
    public class TreatmentDTO : BaseEntityDTO
    {
        public string TreatmentCode { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentCategoryCode { get; set; }
    }
}