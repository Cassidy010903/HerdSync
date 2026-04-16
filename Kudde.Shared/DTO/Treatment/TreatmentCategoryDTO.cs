using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Treatment
{
    public class TreatmentCategoryDTO : BaseEntityDTO
    {
        public string TreatmentCategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}