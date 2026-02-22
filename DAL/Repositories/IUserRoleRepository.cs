using DAL.Models.Authentication;

namespace DAL.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleModel>> GetAllAsync();

        Task<UserRoleModel?> GetByCodeAsync(string code);

        Task<UserRoleModel> AddAsync(UserRoleModel userRole);

        Task<UserRoleModel> UpdateAsync(UserRoleModel userRole);

        Task SoftDeleteAsync(string code);
    }
}