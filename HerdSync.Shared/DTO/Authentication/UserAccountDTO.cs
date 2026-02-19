using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Authentication
{
    public class UserAccountDTO : BaseEntityDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}