using HerdSync.Shared.DTO.Authentication;

namespace BLL.Services
{
    public interface IFarmUserService
    {
        Task<IEnumerable<FarmUserDTO>> GetAllAsync();

        Task<FarmUserDTO?> GetByIdAsync(Guid farmUserId);

        Task<FarmUserDTO> CreateAsync(FarmUserDTO farmUserDTO);

        Task<FarmUserDTO> UpdateAsync(FarmUserDTO farmUserDTO);

        Task SoftDeleteAsync(Guid farmUserId);
    }
}