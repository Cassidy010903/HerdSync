using AutoMapper;
using DAL.Repositories;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalService(IMapper mapper, IAnimalRepository repository, ILogger<AnimalService> logger) : IAnimalService
    {
        public async Task<List<AnimalDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<List<AnimalDTO>>(entities);
        }

        public async Task<AnimalDTO?> GetByIdAsync(Guid animalId)
        {
            var entity = await repository.GetByIdAsync(animalId);
            return entity == null ? null : mapper.Map<AnimalDTO>(entity);
        }

        public async Task<AnimalDTO> CreateAsync(AnimalDTO animalDTO)
        {
            var entity = mapper.Map<AnimalModel>(animalDTO);
            if (entity == null) throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created animal {AnimalId}", created.AnimalId);
            return mapper.Map<AnimalDTO>(created);
        }

        public async Task<AnimalDTO> UpdateAsync(AnimalDTO animalDTO)
        {
            var entity = mapper.Map<AnimalModel>(animalDTO);
            if (entity == null) throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated animal {AnimalId}", updated.AnimalId);
            return mapper.Map<AnimalDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid animalId)
        {
            await repository.SoftDeleteAsync(animalId);
            logger.LogInformation("Soft deleted animal {AnimalId}", animalId);
        }
    }
}