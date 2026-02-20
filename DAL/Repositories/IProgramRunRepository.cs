using DAL.Models.Program.ProgramRun;

namespace DAL.Repositories
{
    public interface IProgramRunRepository
    {
        Task<IEnumerable<ProgramRunModel>> GetAllAsync();
        Task<ProgramRunModel?> GetByIdAsync(Guid programRunId);
        Task<ProgramRunModel> AddAsync(ProgramRunModel programRun);
        Task<ProgramRunModel> UpdateAsync(ProgramRunModel programRun);
        Task SoftDeleteAsync(Guid programRunId);
    }
}