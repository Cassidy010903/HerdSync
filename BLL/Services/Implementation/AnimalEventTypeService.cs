using AutoMapper;
using BLL.Services;
using DAL.Models.Animal;
using DAL.Repositories;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalEventTypeService(IMapper mapper, IAnimalEventTypeRepository repository, ILogger<AnimalEventTypeService> logger) : IAnimalEventTypeService
    {
        public async Task<IEnumerable<AnimalEventTypeDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<AnimalEventTypeDTO>>(entities);
        }

        public async Task<AnimalEventTypeDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<AnimalEventTypeDTO>(entity);
        }

        public async Task<AnimalEventTypeDTO> CreateAsync(AnimalEventTypeDTO animalEventTypeDTO)
        {
            var entity = mapper.Map<AnimalEventTypeModel>(animalEventTypeDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new animal event type with code {Code}", created.EventTypeCode);
            return mapper.Map<AnimalEventTypeDTO>(created);
        }

        public async Task<AnimalEventTypeDTO> UpdateAsync(AnimalEventTypeDTO animalEventTypeDTO)
        {
            var entity = mapper.Map<AnimalEventTypeModel>(animalEventTypeDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated animal event type with code {Code}", updated.EventTypeCode);
            return mapper.Map<AnimalEventTypeDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted animal event type with code {Code}", code);
        }
    }
}