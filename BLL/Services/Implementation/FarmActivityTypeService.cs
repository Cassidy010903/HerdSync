using AutoMapper;
using DAL.Models.Farm;
using DAL.Repositories;
using HerdSync.Shared.DTO.Farm;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class FarmActivityTypeService(IMapper mapper, IFarmActivityTypeRepository repository, ILogger<FarmActivityTypeService> logger) : IFarmActivityTypeService
    {
        public async Task<IEnumerable<FarmActivityTypeDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<FarmActivityTypeDTO>>(entities);
        }

        public async Task<FarmActivityTypeDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<FarmActivityTypeDTO>(entity);
        }

        public async Task<FarmActivityTypeDTO> CreateAsync(FarmActivityTypeDTO farmActivityTypeDTO)
        {
            var entity = mapper.Map<FarmActivityTypeModel>(farmActivityTypeDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new farm activity type with code {Code}", created.FarmActivityTypeCode);
            return mapper.Map<FarmActivityTypeDTO>(created);
        }

        public async Task<FarmActivityTypeDTO> UpdateAsync(FarmActivityTypeDTO farmActivityTypeDTO)
        {
            var entity = mapper.Map<FarmActivityTypeModel>(farmActivityTypeDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated farm activity type with code {Code}", updated.FarmActivityTypeCode);
            return mapper.Map<FarmActivityTypeDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted farm activity type with code {Code}", code);
        }
    }
}