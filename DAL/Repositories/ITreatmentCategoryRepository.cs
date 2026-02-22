using DAL.Models.Treatment;

namespace DAL.Repositories
{
    public interface ITreatmentCategoryRepository
    {
        Task<IEnumerable<TreatmentCategoryModel>> GetAllAsync();

        Task<TreatmentCategoryModel?> GetByCodeAsync(string code);

        Task<TreatmentCategoryModel> AddAsync(TreatmentCategoryModel treatmentCategory);

        Task<TreatmentCategoryModel> UpdateAsync(TreatmentCategoryModel treatmentCategory);

        Task SoftDeleteAsync(string code);
    }
}