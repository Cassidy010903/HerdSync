using DAL.Configuration.Database;
using DAL.Models.Animal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class AnimalTypeRepository(HerdsyncDBContext context, ILogger<AnimalTypeRepository> logger) : IAnimalTypeRepository
    {
        public async Task<IEnumerable<AnimalTypeModel>> GetAllAsync()
        {
            return await context.AnimalTypes
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<AnimalTypeModel?> GetByCodeAsync(string code)
        {
            return await context.AnimalTypes
                .FirstOrDefaultAsync(a => a.AnimalTypeCode == code && !a.IsDeleted);
        }

        public async Task<AnimalTypeModel> AddAsync(AnimalTypeModel animalType)
        {
            context.AnimalTypes.Add(animalType);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new animal type with code {Code}", animalType.AnimalTypeCode);
            return animalType;
        }

        public async Task<AnimalTypeModel> UpdateAsync(AnimalTypeModel animalType)
        {
            context.AnimalTypes.Update(animalType);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal type with code {Code}", animalType.AnimalTypeCode);
            return animalType;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.AnimalTypes.FirstOrDefaultAsync(a => a.AnimalTypeCode == code);
            if (entity == null) throw new KeyNotFoundException($"AnimalType '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted animal type with code {Code}", code);
        }
    }
}