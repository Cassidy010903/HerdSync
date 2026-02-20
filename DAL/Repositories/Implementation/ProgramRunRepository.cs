using DAL.Configuration.Database;
using DAL.Models.Program.ProgramRun;

namespace DAL.Repositories.Implementation
{
    public class ProgramRunRepository(HerdSyncDbContext context, ILogger<ProgramRunRepository> logger) : IProgramRunRepository
    {
        public async Task<IEnumerable<ProgramRunModel>> GetAllAsync()
        {
            return await context.ProgramRuns
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramRunModel?> GetByIdAsync(Guid programRunId)
        {
            return await context.ProgramRuns
                .FirstOrDefaultAsync(p => p.ProgramRunId == programRunId && !p.IsDeleted);
        }

        public async Task<ProgramRunModel> AddAsync(ProgramRunModel programRun)
        {
            context.ProgramRuns.Add(programRun);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program run with ID {ProgramRunId}", programRun.ProgramRunId);
            return programRun;
        }

        public async Task<ProgramRunModel> UpdateAsync(ProgramRunModel programRun)
        {
            context.ProgramRuns.Update(programRun);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program run with ID {ProgramRunId}", programRun.ProgramRunId);
            return programRun;
        }

        public async Task SoftDeleteAsync(Guid programRunId)
        {
            var entity = await context.ProgramRuns.FirstOrDefaultAsync(p => p.ProgramRunId == programRunId);
            if (entity == null) throw new KeyNotFoundException($"ProgramRun {programRunId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program run with ID {ProgramRunId}", programRunId);
        }
    }
}