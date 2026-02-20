using DAL.Models.Treatment;

namespace DAL.Repositories
{
    public interface ITreatmentProductRepository
    {
        Task<IEnumerable<TreatmentProductModel>> GetAllAsync();
        Task<TreatmentProductModel?> GetByIdAsync(Guid treatmentProductId);
        Task<TreatmentProductModel> AddAsync(TreatmentProductModel treatmentProduct);
        Task<TreatmentProductModel> UpdateAsync(TreatmentProductModel treatmentProduct);
        Task SoftDeleteAsync(Guid treatmentProductId);
    }
}