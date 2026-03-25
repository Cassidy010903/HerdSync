using DAL.Models.Authentication;

namespace DAL.Repositories
{
    public interface IUserAccountRepository
    {
        Task<IEnumerable<UserAccountModel>> GetAllAsync();

        Task<UserAccountModel?> GetByIdAsync(Guid userAccountId);

        Task<UserAccountModel> AddAsync(UserAccountModel userAccount);

        Task<UserAccountModel> UpdateAsync(UserAccountModel userAccount);

        Task SoftDeleteAsync(Guid userAccountId);
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}