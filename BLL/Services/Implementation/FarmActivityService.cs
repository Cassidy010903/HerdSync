using AutoMapper;
using DAL.Models.Farm;
using DAL.Repositories;
using HerdSync.Shared.DTO.Farm;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class FarmActivityService(IMapper mapper, IFarmActivityRepository repository, ILogger<FarmActivityService> logger) : IFarmActivityService
    {
        public async Task<IEnumerable<FarmActivityDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<FarmActivityDTO>>(entities);
        }

        public async Task<FarmActivityDTO?> GetByIdAsync(Guid farmActivityId)
        {
            var entity = await repository.GetByIdAsync(farmActivityId);
            return entity == null ? null : mapper.Map<FarmActivityDTO>(entity);
        }

        public async Task<FarmActivityDTO> CreateAsync(FarmActivityDTO farmActivityDTO)
        {
            var entity = mapper.Map<FarmActivityModel>(farmActivityDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new farm activity with ID {FarmActivityId}", created.FarmActivityId);
            return mapper.Map<FarmActivityDTO>(created);
        }

        public async Task<FarmActivityDTO> UpdateAsync(FarmActivityDTO farmActivityDTO)
        {
            var entity = mapper.Map<FarmActivityModel>(farmActivityDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated farm activity with ID {FarmActivityId}", updated.FarmActivityId);
            return mapper.Map<FarmActivityDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid farmActivityId)
        {
            await repository.SoftDeleteAsync(farmActivityId);
            logger.LogInformation("Soft deleted farm activity with ID {FarmActivityId}", farmActivityId);
        }
    }
}