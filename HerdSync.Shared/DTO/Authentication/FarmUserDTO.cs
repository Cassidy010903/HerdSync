using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Authentication
{
    public class FarmUserDTO : BaseEntityDTO
    {
        public Guid FarmUserId { get; set; }
        public Guid FarmId { get; set; }
        public Guid UserId { get; set; }
        public string RoleCode { get; set; }
        public bool IsActive { get; set; }
    }
}