using DAL.Configuration.Database;
using DAL.Models.Farm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class FarmActivityTypeRepository(HerdsyncDBContext context, ILogger<FarmActivityTypeRepository> logger) : IFarmActivityTypeRepository
    {
        public async Task<IEnumerable<FarmActivityTypeModel>> GetAllAsync()
        {
            return await context.FarmActivityTypes
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<FarmActivityTypeModel?> GetByCodeAsync(string code)
        {
            return await context.FarmActivityTypes
                .FirstOrDefaultAsync(f => f.FarmActivityTypeCode == code && !f.IsDeleted);
        }

        public async Task<FarmActivityTypeModel> AddAsync(FarmActivityTypeModel farmActivityType)
        {
            context.FarmActivityTypes.Add(farmActivityType);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new farm activity type with code {Code}", farmActivityType.FarmActivityTypeCode);
            return farmActivityType;
        }

        public async Task<FarmActivityTypeModel> UpdateAsync(FarmActivityTypeModel farmActivityType)
        {
            context.FarmActivityTypes.Update(farmActivityType);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated farm activity type with code {Code}", farmActivityType.FarmActivityTypeCode);
            return farmActivityType;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.FarmActivityTypes.FirstOrDefaultAsync(f => f.FarmActivityTypeCode == code);
            if (entity == null) throw new KeyNotFoundException($"FarmActivityType '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted farm activity type with code {Code}", code);
        }
    }
}