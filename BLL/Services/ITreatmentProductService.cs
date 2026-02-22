using HerdSync.Shared.DTO.Treatment;

namespace BLL.Services
{
    public interface ITreatmentProductService
    {
        Task<IEnumerable<TreatmentProductDTO>> GetAllAsync();

        Task<TreatmentProductDTO?> GetByIdAsync(Guid treatmentProductId);

        Task<TreatmentProductDTO> CreateAsync(TreatmentProductDTO treatmentProductDTO);

        Task<TreatmentProductDTO> UpdateAsync(TreatmentProductDTO treatmentProductDTO);

        Task SoftDeleteAsync(Guid treatmentProductId);
    }
}