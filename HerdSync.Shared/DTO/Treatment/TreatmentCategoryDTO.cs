using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Treatment
{
    public class TreatmentCategoryDTO : BaseEntityDTO
    {
        public string TreatmentCategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}