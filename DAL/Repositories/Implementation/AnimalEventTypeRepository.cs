using DAL.Configuration.Database;
using DAL.Models.Animal;

namespace DAL.Repositories.Implementation
{
    public class AnimalEventTypeRepository(HerdSyncDbContext context, ILogger<AnimalEventTypeRepository> logger) : IAnimalEventTypeRepository
    {
        public async Task<IEnumerable<AnimalEventTypeModel>> GetAllAsync()
        {
            return await context.AnimalEventTypes
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<AnimalEventTypeModel?> GetByCodeAsync(string code)
        {
            return await context.AnimalEventTypes
                .FirstOrDefaultAsync(a => a.EventTypeCode == code && !a.IsDeleted);
        }

        public async Task<AnimalEventTypeModel> AddAsync(AnimalEventTypeModel animalEventType)
        {
            context.AnimalEventTypes.Add(animalEventType);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new animal event type with code {Code}", animalEventType.EventTypeCode);
            return animalEventType;
        }

        public async Task<AnimalEventTypeModel> UpdateAsync(AnimalEventTypeModel animalEventType)
        {
            context.AnimalEventTypes.Update(animalEventType);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal event type with code {Code}", animalEventType.EventTypeCode);
            return animalEventType;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.AnimalEventTypes.FirstOrDefaultAsync(a => a.EventTypeCode == code);
            if (entity == null) throw new KeyNotFoundException($"AnimalEventType '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted animal event type with code {Code}", code);
        }
    }
}