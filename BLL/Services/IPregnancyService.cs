using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IPregnancyService
    {
        Task<IEnumerable<PregnancyDTO>> GetAllAsync();

        Task<PregnancyDTO?> GetByIdAsync(Guid pregnancyId);

        Task<PregnancyDTO> CreateAsync(PregnancyDTO pregnancyDTO);

        Task<PregnancyDTO> UpdateAsync(PregnancyDTO pregnancyDTO);

        Task SoftDeleteAsync(Guid pregnancyId);
    }
}