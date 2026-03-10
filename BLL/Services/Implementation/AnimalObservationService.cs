using AutoMapper;
using DAL.Models.Animal;
using DAL.Repositories;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;
namespace BLL.Services.Implementation
{
    public class AnimalObservationService(IMapper mapper, IAnimalObservationRepository repository, ILogger<AnimalObservationService> logger) : IAnimalObservationService
    {
        public async Task<IEnumerable<AnimalObservationDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<AnimalObservationDTO>>(entities);
        }

        public async Task<AnimalObservationDTO?> GetByIdAsync(Guid animalObservationId)
        {
            var entity = await repository.GetByIdAsync(animalObservationId);
            return entity == null ? null : mapper.Map<AnimalObservationDTO>(entity);
        }

        public async Task<AnimalObservationDTO> CreateAsync(AnimalObservationDTO animalObservationDTO)
        {
            var entity = mapper.Map<AnimalObservationModel>(animalObservationDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new animal observation with ID {AnimalObservationId}", created.AnimalObservationId);
            return mapper.Map<AnimalObservationDTO>(created);
        }

        public async Task<AnimalObservationDTO> UpdateAsync(AnimalObservationDTO animalObservationDTO)
        {
            var entity = mapper.Map<AnimalObservationModel>(animalObservationDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated animal observation with ID {AnimalObservationId}", updated.AnimalObservationId);
            return mapper.Map<AnimalObservationDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid animalObservationId)
        {
            await repository.SoftDeleteAsync(animalObservationId);
            logger.LogInformation("Soft deleted animal observation with ID {AnimalObservationId}", animalObservationId);
        }
    }
}