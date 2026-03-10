using DAL.Configuration.Database;
using DAL.Models.Animal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace DAL.Repositories.Implementation
{
    public class AnimalObservationRepository(HerdsyncDBContext context, ILogger<AnimalObservationRepository> logger) : IAnimalObservationRepository
    {
        public async Task<IEnumerable<AnimalObservationModel>> GetAllAsync()
        {
            return await context.AnimalObservations
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<AnimalObservationModel?> GetByIdAsync(Guid animalObservationId)
        {
            return await context.AnimalObservations
                .FirstOrDefaultAsync(a => a.AnimalObservationId == animalObservationId && !a.IsDeleted);
        }

        public async Task<AnimalObservationModel> AddAsync(AnimalObservationModel animalObservation)
        {
            context.AnimalObservations.Add(animalObservation);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new animal observation with ID {AnimalObservationId}", animalObservation.AnimalObservationId);
            return animalObservation;
        }

        public async Task<AnimalObservationModel> UpdateAsync(AnimalObservationModel animalObservation)
        {
            var existing = await context.AnimalObservations
                .FirstOrDefaultAsync(a => a.AnimalObservationId == animalObservation.AnimalObservationId);
            if (existing == null)
                throw new KeyNotFoundException($"AnimalObservation {animalObservation.AnimalObservationId} not found.");

            existing.ConditionCode = animalObservation.ConditionCode;
            existing.NumericValue = animalObservation.NumericValue;
            existing.TextValue = animalObservation.TextValue;
            existing.Flag = animalObservation.Flag;
            existing.Notes = animalObservation.Notes;
            existing.ObservationDate = animalObservation.ObservationDate;

            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal observation with ID {AnimalObservationId}", existing.AnimalObservationId);
            return existing;
        }

        public async Task SoftDeleteAsync(Guid animalObservationId)
        {
            var entity = await context.AnimalObservations
                .FirstOrDefaultAsync(a => a.AnimalObservationId == animalObservationId);
            if (entity == null)
                throw new KeyNotFoundException($"AnimalObservation {animalObservationId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted animal observation with ID {AnimalObservationId}", animalObservationId);
        }
    }
}