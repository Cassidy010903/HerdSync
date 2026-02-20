using HerdSync.Shared.DTO.Treatment;

namespace BLL.Services
{
    public interface ITreatmentService
    {
        Task<IEnumerable<TreatmentDTO>> GetAllAsync();
        Task<TreatmentDTO?> GetByIdAsync(string code);
        Task<TreatmentDTO> CreateAsync(TreatmentDTO treatmentDTO);
        Task<TreatmentDTO> UpdateAsync(TreatmentDTO treatmentDTO);
        Task SoftDeleteAsync(string code);
    }
}