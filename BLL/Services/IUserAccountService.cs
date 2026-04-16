using Kudde.Shared.DTO.Authentication;

namespace BLL.Services
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserAccountDTO>> GetAllAsync();

        Task<UserAccountDTO?> GetByIdAsync(Guid userAccountId);

        Task<UserAccountDTO> CreateAsync(UserAccountDTO userAccountDTO);

        Task<UserAccountDTO> UpdateAsync(UserAccountDTO userAccountDTO);

        Task SoftDeleteAsync(Guid userAccountId);
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}