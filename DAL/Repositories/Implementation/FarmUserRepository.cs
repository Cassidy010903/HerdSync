using DAL.Configuration.Database;
using DAL.Models.Authentication;

namespace DAL.Repositories.Implementation
{
    public class FarmUserRepository(HerdSyncDbContext context, ILogger<FarmUserRepository> logger) : IFarmUserRepository
    {
        public async Task<IEnumerable<FarmUserModel>> GetAllAsync()
        {
            return await context.FarmUsers
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<FarmUserModel?> GetByIdAsync(Guid farmUserId)
        {
            return await context.FarmUsers
                .FirstOrDefaultAsync(f => f.UserId == farmUserId && !f.IsDeleted);
        }

        public async Task<FarmUserModel> AddAsync(FarmUserModel farmUser)
        {
            context.FarmUsers.Add(farmUser);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new farm user with ID {FarmUserId}", farmUser.UserId);
            return farmUser;
        }

        public async Task<FarmUserModel> UpdateAsync(FarmUserModel farmUser)
        {
            context.FarmUsers.Update(farmUser);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated farm user with ID {FarmUserId}", farmUser.UserId);
            return farmUser;
        }

        public async Task SoftDeleteAsync(Guid farmUserId)
        {
            var entity = await context.FarmUsers.FirstOrDefaultAsync(f => f.UserId == farmUserId);
            if (entity == null) throw new KeyNotFoundException($"FarmUser {farmUserId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted farm user with ID {FarmUserId}", farmUserId);
        }
    }
}