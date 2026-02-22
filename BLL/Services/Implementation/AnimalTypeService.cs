using AutoMapper;
using DAL.Models.Animal;
using DAL.Repositories;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalTypeService(IMapper mapper, IAnimalTypeRepository repository, ILogger<AnimalTypeService> logger) : IAnimalTypeService
    {
        public async Task<IEnumerable<AnimalTypeDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<AnimalTypeDTO>>(entities);
        }

        public async Task<AnimalTypeDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<AnimalTypeDTO>(entity);
        }

        public async Task<AnimalTypeDTO> CreateAsync(AnimalTypeDTO animalTypeDTO)
        {
            var entity = mapper.Map<AnimalTypeModel>(animalTypeDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new animal type with code {Code}", created.AnimalTypeCode);
            return mapper.Map<AnimalTypeDTO>(created);
        }

        public async Task<AnimalTypeDTO> UpdateAsync(AnimalTypeDTO animalTypeDTO)
        {
            var entity = mapper.Map<AnimalTypeModel>(animalTypeDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated animal type with code {Code}", updated.AnimalTypeCode);
            return mapper.Map<AnimalTypeDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted animal type with code {Code}", code);
        }
    }
}