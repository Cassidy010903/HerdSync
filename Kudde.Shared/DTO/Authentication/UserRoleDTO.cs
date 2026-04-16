using Kudde.Shared.DTO.Base;

namespace Kudde.Shared.DTO.Authentication
{
    public class UserRoleDTO : BaseEntityDTO
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}