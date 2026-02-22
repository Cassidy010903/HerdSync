using HerdSync.Shared.DTO.Farm;

namespace BLL.Services
{
    public interface IFarmActivityService
    {
        Task<IEnumerable<FarmActivityDTO>> GetAllAsync();

        Task<FarmActivityDTO?> GetByIdAsync(Guid farmActivityId);

        Task<FarmActivityDTO> CreateAsync(FarmActivityDTO farmActivityDTO);

        Task<FarmActivityDTO> UpdateAsync(FarmActivityDTO farmActivityDTO);

        Task SoftDeleteAsync(Guid farmActivityId);
    }
}