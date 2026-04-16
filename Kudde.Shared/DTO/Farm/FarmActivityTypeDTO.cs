using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Farm
{
    public class FarmActivityTypeDTO : BaseEntityDTO
    {
        public string FarmActivityTypeCode { get; set; }
        public string ActivityTypeName { get; set; }
        public string Description { get; set; }
    }
}