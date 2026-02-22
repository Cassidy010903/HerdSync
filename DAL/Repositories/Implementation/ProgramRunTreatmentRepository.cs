using DAL.Configuration.Database;
using DAL.Models.Program.ProgramRun;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class ProgramRunTreatmentRepository(HerdsyncDBContext context, ILogger<ProgramRunTreatmentRepository> logger) : IProgramRunTreatmentRepository
    {
        public async Task<IEnumerable<ProgramRunTreatmentModel>> GetAllAsync()
        {
            return await context.ProgramRunTreatments
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramRunTreatmentModel?> GetByIdAsync(Guid programRunTreatmentId)
        {
            return await context.ProgramRunTreatments
                .FirstOrDefaultAsync(p => p.ProgramRunTreatmentId == programRunTreatmentId && !p.IsDeleted);
        }

        public async Task<ProgramRunTreatmentModel> AddAsync(ProgramRunTreatmentModel programRunTreatment)
        {
            context.ProgramRunTreatments.Add(programRunTreatment);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program run treatment with ID {ProgramRunTreatmentId}", programRunTreatment.ProgramRunTreatmentId);
            return programRunTreatment;
        }

        public async Task<ProgramRunTreatmentModel> UpdateAsync(ProgramRunTreatmentModel programRunTreatment)
        {
            context.ProgramRunTreatments.Update(programRunTreatment);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program run treatment with ID {ProgramRunTreatmentId}", programRunTreatment.ProgramRunTreatmentId);
            return programRunTreatment;
        }

        public async Task SoftDeleteAsync(Guid programRunTreatmentId)
        {
            var entity = await context.ProgramRunTreatments.FirstOrDefaultAsync(p => p.ProgramRunTreatmentId == programRunTreatmentId);
            if (entity == null) throw new KeyNotFoundException($"ProgramRunTreatment {programRunTreatmentId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program run treatment with ID {ProgramRunTreatmentId}", programRunTreatmentId);
        }
    }
}