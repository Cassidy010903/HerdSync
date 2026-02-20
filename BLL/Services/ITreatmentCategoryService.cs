using HerdSync.Shared.DTO.Treatment;

namespace BLL.Services
{
    public interface ITreatmentCategoryService
    {
        Task<IEnumerable<TreatmentCategoryDTO>> GetAllAsync();
        Task<TreatmentCategoryDTO?> GetByIdAsync(string code);
        Task<TreatmentCategoryDTO> CreateAsync(TreatmentCategoryDTO treatmentCategoryDTO);
        Task<TreatmentCategoryDTO> UpdateAsync(TreatmentCategoryDTO treatmentCategoryDTO);
        Task SoftDeleteAsync(string code);
    }
}