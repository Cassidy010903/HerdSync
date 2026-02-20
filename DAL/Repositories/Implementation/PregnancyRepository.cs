using DAL.Configuration.Database;
using DAL.Models.Animal;

namespace DAL.Repositories.Implementation
{
    public class PregnancyRepository(HerdSyncDbContext context, ILogger<PregnancyRepository> logger) : IPregnancyRepository
    {
        public async Task<IEnumerable<PregnancyModel>> GetAllAsync()
        {
            return await context.Pregnancies
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<PregnancyModel?> GetByIdAsync(Guid pregnancyId)
        {
            return await context.Pregnancies
                .FirstOrDefaultAsync(p => p.PregnancyId == pregnancyId && !p.IsDeleted);
        }

        public async Task<PregnancyModel> AddAsync(PregnancyModel pregnancy)
        {
            context.Pregnancies.Add(pregnancy);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new pregnancy record with ID {PregnancyId}", pregnancy.PregnancyId);
            return pregnancy;
        }

        public async Task<PregnancyModel> UpdateAsync(PregnancyModel pregnancy)
        {
            context.Pregnancies.Update(pregnancy);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated pregnancy record with ID {PregnancyId}", pregnancy.PregnancyId);
            return pregnancy;
        }

        public async Task SoftDeleteAsync(Guid pregnancyId)
        {
            var entity = await context.Pregnancies.FirstOrDefaultAsync(p => p.PregnancyId == pregnancyId);
            if (entity == null) throw new KeyNotFoundException($"Pregnancy {pregnancyId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted pregnancy record with ID {PregnancyId}", pregnancyId);
        }
    }
}