using AutoMapper;
using BLL.Services;
using DAL.Models.Animal;
using DAL.Services;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalService(IMapper mapper, ISpeciesRepository repository, ILogger<AnimalService> logger) : IAnimalService
    {
        public async Task AddAnimalAsync(AnimalDTO animalDTO)
        {
            var animal = mapper.Map<AnimalModel>(animalDTO);

            if (animal == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");

            await repository.AddSpecies(animal);

            logger.LogInformation("Added new animal with number {CowNumber}", animal.DisplayIdentifier);
        }

        public async Task<List<AnimalDTO>> GetAllHerdAsync()
        {
            var entities = await repository.GetAllSpeciesAsync();
            return mapper.Map<List<AnimalDTO>>(entities);
        }

        public async Task UpdateAnimalAsync(AnimalDTO animalDTO)
        {
            var entity = mapper.Map<AnimalModel>(animalDTO);
            await repository.UpdateSpecies(entity);
        }
    }
    public class AnimalService2(IMapper mapper, IAnimalRepository repository, ILogger<AnimalService> logger) : IAnimalService2
    {
        public async Task<IEnumerable<AnimalDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<AnimalDTO>>(entities);
        }

        public async Task<AnimalDTO?> GetByIdAsync(Guid animalId)
        {
            var entity = await repository.GetByIdAsync(animalId);
            return entity == null ? null : mapper.Map<AnimalDTO>(entity);
        }

        public async Task<AnimalDTO> CreateAsync(AnimalDTO animalDTO)
        {
            var entity = mapper.Map<AnimalModel>(animalDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new animal with ID {AnimalId}", created.AnimalId);
            return mapper.Map<AnimalDTO>(created);
        }

        public async Task<AnimalDTO> UpdateAsync(AnimalDTO animalDTO)
        {
            var entity = mapper.Map<AnimalModel>(animalDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated animal with ID {AnimalId}", updated.AnimalId);
            return mapper.Map<AnimalDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid animalId)
        {
            await repository.SoftDeleteAsync(animalId);
            logger.LogInformation("Soft deleted animal with ID {AnimalId}", animalId);
        }
    }
}