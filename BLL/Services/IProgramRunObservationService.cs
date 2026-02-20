using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramRunObservationService
    {
        Task<IEnumerable<ProgramRunObservationDTO>> GetAllAsync();
        Task<ProgramRunObservationDTO?> GetByIdAsync(Guid programRunObservationId);
        Task<ProgramRunObservationDTO> CreateAsync(ProgramRunObservationDTO programRunObservationDTO);
        Task<ProgramRunObservationDTO> UpdateAsync(ProgramRunObservationDTO programRunObservationDTO);
        Task SoftDeleteAsync(Guid programRunObservationId);
    }
}