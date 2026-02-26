using DAL.Configuration.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class AnimalRepository(HerdsyncDBContext context, ILogger<AnimalRepository> logger) : IAnimalRepository
    {
        public async Task<IEnumerable<AnimalModel>> GetAllAsync()
            => await context.Animals.Where(a => !a.IsDeleted).ToListAsync();

        public async Task<AnimalModel?> GetByIdAsync(Guid animalId)
            => await context.Animals.FirstOrDefaultAsync(a => a.AnimalId == animalId && !a.IsDeleted);

        public async Task<AnimalModel> AddAsync(AnimalModel animal)
        {
            context.Animals.Add(animal);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new animal with ID {AnimalId}", animal.AnimalId);
            return animal;
        }

        public async Task<AnimalModel> UpdateAsync(AnimalModel animal)
        {
            var existing = await context.Animals.FirstOrDefaultAsync(a => a.AnimalId == animal.AnimalId);
            if (existing == null) throw new KeyNotFoundException($"Animal {animal.AnimalId} not found.");

            existing.DisplayIdentifier = animal.DisplayIdentifier;
            existing.AnimalTypeCode = animal.AnimalTypeCode;
            existing.BirthYear = animal.BirthYear;
            existing.Gender = animal.Gender;
            existing.MotherAnimalId = animal.MotherAnimalId;
            existing.FatherAnimalId = animal.FatherAnimalId;
            existing.IsDeleted = animal.IsDeleted;

            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal with ID {AnimalId}", existing.AnimalId);
            return existing;
        }

        public async Task SoftDeleteAsync(Guid animalId)
        {
            var entity = await context.Animals.FirstOrDefaultAsync(a => a.AnimalId == animalId);
            if (entity == null) throw new KeyNotFoundException($"Animal {animalId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted animal with ID {AnimalId}", animalId);
        }
    }
}