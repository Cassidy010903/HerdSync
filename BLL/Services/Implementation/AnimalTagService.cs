using AutoMapper;
using BLL.Services;
using DAL.Models.Animal;
using DAL.Repositories;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalTagService(IMapper mapper, IAnimalTagRepository repository, ILogger<AnimalTagService> logger) : IAnimalTagService
    {
        public async Task<IEnumerable<AnimalTagDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<AnimalTagDTO>>(entities);
        }

        public async Task<AnimalTagDTO?> GetByIdAsync(Guid animalTagId)
        {
            var entity = await repository.GetByIdAsync(animalTagId);
            return entity == null ? null : mapper.Map<AnimalTagDTO>(entity);
        }

        public async Task<AnimalTagDTO> CreateAsync(AnimalTagDTO animalTagDTO)
        {
            var entity = mapper.Map<AnimalTagModel>(animalTagDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new animal tag with ID {AnimalTagId}", created.AnimalTagId);
            return mapper.Map<AnimalTagDTO>(created);
        }

        public async Task<AnimalTagDTO> UpdateAsync(AnimalTagDTO animalTagDTO)
        {
            var entity = mapper.Map<AnimalTagModel>(animalTagDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated animal tag with ID {AnimalTagId}", updated.AnimalTagId);
            return mapper.Map<AnimalTagDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid animalTagId)
        {
            await repository.SoftDeleteAsync(animalTagId);
            logger.LogInformation("Soft deleted animal tag with ID {AnimalTagId}", animalTagId);
        }
    }
}