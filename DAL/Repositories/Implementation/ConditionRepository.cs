using DAL.Configuration.Database;
using DAL.Models.Treatment;

namespace DAL.Repositories.Implementation
{
    public class ConditionRepository(HerdSyncDbContext context, ILogger<ConditionRepository> logger) : IConditionRepository
    {
        public async Task<IEnumerable<ConditionModel>> GetAllAsync()
        {
            return await context.Conditions
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<ConditionModel?> GetByCodeAsync(string code)
        {
            return await context.Conditions
                .FirstOrDefaultAsync(c => c.ConditionCode == code && !c.IsDeleted);
        }

        public async Task<ConditionModel> AddAsync(ConditionModel condition)
        {
            context.Conditions.Add(condition);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new condition with code {ConditionCode}", condition.ConditionCode);
            return condition;
        }

        public async Task<ConditionModel> UpdateAsync(ConditionModel condition)
        {
            context.Conditions.Update(condition);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated condition with code {ConditionCode}", condition.ConditionCode);
            return condition;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.Conditions.FirstOrDefaultAsync(c => c.ConditionCode == code);
            if (entity == null) throw new KeyNotFoundException($"Condition '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted condition with code {ConditionCode}", code);
        }
    }
}