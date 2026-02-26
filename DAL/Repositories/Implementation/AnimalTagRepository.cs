using DAL.Configuration.Database;
using DAL.Models.Animal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class AnimalTagRepository(HerdsyncDBContext context, ILogger<AnimalTagRepository> logger) : IAnimalTagRepository
    {
        public async Task<IEnumerable<AnimalTagModel>> GetAllAsync()
        {
            return await context.AnimalTags
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<AnimalTagModel?> GetByIdAsync(Guid animalTagId)
        {
            return await context.AnimalTags
                .FirstOrDefaultAsync(a => a.AnimalTagId == animalTagId && !a.IsDeleted);
        }

        public async Task<AnimalTagModel> AddAsync(AnimalTagModel animalTag)
        {
            context.AnimalTags.Add(animalTag);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new animal tag with ID {AnimalTagId}", animalTag.AnimalTagId);
            return animalTag;
        }

        public async Task<AnimalTagModel> UpdateAsync(AnimalTagModel animalTag)
        {
            var existing = await context.AnimalTags.FirstOrDefaultAsync(a => a.AnimalTagId == animalTag.AnimalTagId);
            if (existing == null) throw new KeyNotFoundException($"AnimalTag {animalTag.AnimalTagId} not found.");

            existing.RFIDTagCode = animalTag.RFIDTagCode;
            existing.AnimalId = animalTag.AnimalId;
            existing.AssignedDate = animalTag.AssignedDate;
            existing.UnassignedDate = animalTag.UnassignedDate;
            existing.IsCurrent = animalTag.IsCurrent;

            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal tag with ID {AnimalTagId}", existing.AnimalTagId);
            return existing;
        }

        public async Task SoftDeleteAsync(Guid animalTagId)
        {
            var entity = await context.AnimalTags.FirstOrDefaultAsync(a => a.AnimalTagId == animalTagId);
            if (entity == null) throw new KeyNotFoundException($"AnimalTag {animalTagId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted animal tag with ID {AnimalTagId}", animalTagId);
        }
    }
}