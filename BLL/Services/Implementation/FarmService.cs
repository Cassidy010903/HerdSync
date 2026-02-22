using AutoMapper;
using DAL.Models.Farm;
using DAL.Repositories;
using HerdSync.Shared.DTO.Farm;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class FarmService(IMapper mapper, IFarmRepository repository, ILogger<FarmService> logger) : IFarmService
    {
        public async Task<IEnumerable<FarmDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<FarmDTO>>(entities);
        }

        public async Task<FarmDTO?> GetByIdAsync(Guid farmId)
        {
            var entity = await repository.GetByIdAsync(farmId);
            return entity == null ? null : mapper.Map<FarmDTO>(entity);
        }

        public async Task<FarmDTO> CreateAsync(FarmDTO farmDTO)
        {
            var entity = mapper.Map<FarmModel>(farmDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new farm with ID {FarmId}", created.FarmId);
            return mapper.Map<FarmDTO>(created);
        }

        public async Task<FarmDTO> UpdateAsync(FarmDTO farmDTO)
        {
            var entity = mapper.Map<FarmModel>(farmDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated farm with ID {FarmId}", updated.FarmId);
            return mapper.Map<FarmDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid farmId)
        {
            await repository.SoftDeleteAsync(farmId);
            logger.LogInformation("Soft deleted farm with ID {FarmId}", farmId);
        }
    }
}