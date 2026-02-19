using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Farm
{
    public class FarmActivityTypeDTO : BaseEntityDTO
    {
        public string FarmActivityTypeCode { get; set; }
        public string ActivityTypeName { get; set; }
        public string Description { get; set; }
    }
}