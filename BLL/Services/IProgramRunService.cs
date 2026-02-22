using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramRunService
    {
        Task<List<ProgramRunDTO>> GetAllAsync();
        Task<ProgramRunDTO?> GetByIdAsync(Guid programRunId);
        Task<ProgramRunDTO> CreateAsync(ProgramRunDTO programRunDTO);
        Task<ProgramRunDTO> UpdateAsync(ProgramRunDTO programRunDTO);
        Task SoftDeleteAsync(Guid programRunId);
    }
}