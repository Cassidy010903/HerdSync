using DAL.Configuration.Database;
using DAL.Models.Farm;

namespace DAL.Repositories.Implementation
{
    public class FarmActivityRepository(HerdSyncDbContext context, ILogger<FarmActivityRepository> logger) : IFarmActivityRepository
    {
        public async Task<IEnumerable<FarmActivityModel>> GetAllAsync()
        {
            return await context.FarmActivities
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<FarmActivityModel?> GetByIdAsync(Guid farmActivityId)
        {
            return await context.FarmActivities
                .FirstOrDefaultAsync(f => f.FarmActivityId == farmActivityId && !f.IsDeleted);
        }

        public async Task<FarmActivityModel> AddAsync(FarmActivityModel farmActivity)
        {
            context.FarmActivities.Add(farmActivity);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new farm activity with ID {FarmActivityId}", farmActivity.FarmActivityId);
            return farmActivity;
        }

        public async Task<FarmActivityModel> UpdateAsync(FarmActivityModel farmActivity)
        {
            context.FarmActivities.Update(farmActivity);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated farm activity with ID {FarmActivityId}", farmActivity.FarmActivityId);
            return farmActivity;
        }

        public async Task SoftDeleteAsync(Guid farmActivityId)
        {
            var entity = await context.FarmActivities.FirstOrDefaultAsync(f => f.FarmActivityId == farmActivityId);
            if (entity == null) throw new KeyNotFoundException($"FarmActivity {farmActivityId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted farm activity with ID {FarmActivityId}", farmActivityId);
        }
    }
}