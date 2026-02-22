using DAL.Models.Treatment;

namespace DAL.Repositories
{
    public interface ITreatmentRepository
    {
        Task<IEnumerable<TreatmentModel>> GetAllAsync();

        Task<TreatmentModel?> GetByCodeAsync(string code);

        Task<TreatmentModel> AddAsync(TreatmentModel treatment);

        Task<TreatmentModel> UpdateAsync(TreatmentModel treatment);

        Task SoftDeleteAsync(string code);
    }
}