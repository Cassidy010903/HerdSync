using DAL.Configuration.Database;
using DAL.Models.Program.ProgramRun;

namespace DAL.Repositories.Implementation
{
    public class ProgramRunAnimalRepository(HerdSyncDbContext context, ILogger<ProgramRunAnimalRepository> logger) : IProgramRunAnimalRepository
    {
        public async Task<IEnumerable<ProgramRunAnimalModel>> GetAllAsync()
        {
            return await context.ProgramRunAnimals
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramRunAnimalModel?> GetByIdAsync(Guid programRunAnimalId)
        {
            return await context.ProgramRunAnimals
                .FirstOrDefaultAsync(p => p.ProgramRunAnimalId == programRunAnimalId && !p.IsDeleted);
        }

        public async Task<ProgramRunAnimalModel> AddAsync(ProgramRunAnimalModel programRunAnimal)
        {
            context.ProgramRunAnimals.Add(programRunAnimal);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program run animal with ID {ProgramRunAnimalId}", programRunAnimal.ProgramRunAnimalId);
            return programRunAnimal;
        }

        public async Task<ProgramRunAnimalModel> UpdateAsync(ProgramRunAnimalModel programRunAnimal)
        {
            context.ProgramRunAnimals.Update(programRunAnimal);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program run animal with ID {ProgramRunAnimalId}", programRunAnimal.ProgramRunAnimalId);
            return programRunAnimal;
        }

        public async Task SoftDeleteAsync(Guid programRunAnimalId)
        {
            var entity = await context.ProgramRunAnimals.FirstOrDefaultAsync(p => p.ProgramRunAnimalId == programRunAnimalId);
            if (entity == null) throw new KeyNotFoundException($"ProgramRunAnimal {programRunAnimalId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program run animal with ID {ProgramRunAnimalId}", programRunAnimalId);
        }
    }
}