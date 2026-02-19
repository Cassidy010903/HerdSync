using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Authentication
{
    public class UserRoleDTO : BaseEntityDTO
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}