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
            context.Animals.Update(animal);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal with ID {AnimalId}", animal.AnimalId);
            return animal;
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