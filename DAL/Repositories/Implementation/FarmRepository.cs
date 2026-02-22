using DAL.Configuration.Database;
using DAL.Models.Farm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class FarmRepository(HerdsyncDBContext context, ILogger<FarmRepository> logger) : IFarmRepository
    {
        public async Task<IEnumerable<FarmModel>> GetAllAsync()
        {
            return await context.Farms
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<FarmModel?> GetByIdAsync(Guid farmId)
        {
            return await context.Farms
                .FirstOrDefaultAsync(f => f.FarmId == farmId && !f.IsDeleted);
        }

        public async Task<FarmModel> AddAsync(FarmModel farm)
        {
            context.Farms.Add(farm);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new farm with ID {FarmId}", farm.FarmId);
            return farm;
        }

        public async Task<FarmModel> UpdateAsync(FarmModel farm)
        {
            context.Farms.Update(farm);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated farm with ID {FarmId}", farm.FarmId);
            return farm;
        }

        public async Task SoftDeleteAsync(Guid farmId)
        {
            var entity = await context.Farms.FirstOrDefaultAsync(f => f.FarmId == farmId);
            if (entity == null) throw new KeyNotFoundException($"Farm {farmId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted farm with ID {FarmId}", farmId);
        }
    }
}