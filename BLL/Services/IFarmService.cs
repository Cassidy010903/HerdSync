using HerdSync.Shared.DTO.Farm;

namespace BLL.Services
{
    public interface IFarmService
    {
        Task<IEnumerable<FarmDTO>> GetAllAsync();

        Task<FarmDTO?> GetByIdAsync(Guid farmId);

        Task<FarmDTO> CreateAsync(FarmDTO farmDTO);

        Task<FarmDTO> UpdateAsync(FarmDTO farmDTO);

        Task SoftDeleteAsync(Guid farmId);
    }
}