using DAL.Configuration.Database;
using DAL.Models.Program.ProgramRun;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class ProgramRunObservationRepository(HerdsyncDBContext context, ILogger<ProgramRunObservationRepository> logger) : IProgramRunObservationRepository
    {
        public async Task<IEnumerable<ProgramRunObservationModel>> GetAllAsync()
        {
            return await context.ProgramRunObservations
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramRunObservationModel?> GetByIdAsync(Guid programRunObservationId)
        {
            return await context.ProgramRunObservations
                .FirstOrDefaultAsync(p => p.ProgramRunObservationId == programRunObservationId && !p.IsDeleted);
        }

        public async Task<ProgramRunObservationModel> AddAsync(ProgramRunObservationModel programRunObservation)
        {
            context.ProgramRunObservations.Add(programRunObservation);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program run observation with ID {ProgramRunObservationId}", programRunObservation.ProgramRunObservationId);
            return programRunObservation;
        }

        public async Task<ProgramRunObservationModel> UpdateAsync(ProgramRunObservationModel programRunObservation)
        {
            context.ProgramRunObservations.Update(programRunObservation);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program run observation with ID {ProgramRunObservationId}", programRunObservation.ProgramRunObservationId);
            return programRunObservation;
        }

        public async Task SoftDeleteAsync(Guid programRunObservationId)
        {
            var entity = await context.ProgramRunObservations.FirstOrDefaultAsync(p => p.ProgramRunObservationId == programRunObservationId);
            if (entity == null) throw new KeyNotFoundException($"ProgramRunObservation {programRunObservationId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program run observation with ID {ProgramRunObservationId}", programRunObservationId);
        }
    }
}