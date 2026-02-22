using HerdSync.Shared.DTO.Authentication;

namespace BLL.Services
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDTO>> GetAllAsync();

        Task<UserRoleDTO?> GetByIdAsync(string code);

        Task<UserRoleDTO> CreateAsync(UserRoleDTO userRoleDTO);

        Task<UserRoleDTO> UpdateAsync(UserRoleDTO userRoleDTO);

        Task SoftDeleteAsync(string code);
    }
}