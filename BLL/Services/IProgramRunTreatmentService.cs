using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramRunTreatmentService
    {
        Task<IEnumerable<ProgramRunTreatmentDTO>> GetAllAsync();
        Task<ProgramRunTreatmentDTO?> GetByIdAsync(Guid programRunTreatmentId);
        Task<ProgramRunTreatmentDTO> CreateAsync(ProgramRunTreatmentDTO programRunTreatmentDTO);
        Task<ProgramRunTreatmentDTO> UpdateAsync(ProgramRunTreatmentDTO programRunTreatmentDTO);
        Task SoftDeleteAsync(Guid programRunTreatmentId);
    }
}