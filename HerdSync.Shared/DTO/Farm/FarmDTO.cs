using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Farm
{
    public class FarmDTO : BaseEntityDTO
    {
        public Guid FarmId { get; set; }
        public string FarmName { get; set; }
        public Guid OwnerUserId { get; set; }
    }
}