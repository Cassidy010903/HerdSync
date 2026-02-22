using HerdSync.Shared.DTO.Farm;

namespace BLL.Services
{
    public interface IFarmActivityTypeService
    {
        Task<IEnumerable<FarmActivityTypeDTO>> GetAllAsync();

        Task<FarmActivityTypeDTO?> GetByIdAsync(string code);

        Task<FarmActivityTypeDTO> CreateAsync(FarmActivityTypeDTO farmActivityTypeDTO);

        Task<FarmActivityTypeDTO> UpdateAsync(FarmActivityTypeDTO farmActivityTypeDTO);

        Task SoftDeleteAsync(string code);
    }
}