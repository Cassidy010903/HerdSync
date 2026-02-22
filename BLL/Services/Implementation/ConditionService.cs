using AutoMapper;
using DAL.Models.Treatment;
using DAL.Repositories;
using HerdSync.Shared.DTO.Treatment;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class ConditionService(IMapper mapper, IConditionRepository repository, ILogger<ConditionService> logger) : IConditionService
    {
        public async Task<IEnumerable<ConditionDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ConditionDTO>>(entities);
        }

        public async Task<ConditionDTO?> GetBycodeAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<ConditionDTO>(entity);
        }

        public async Task<ConditionDTO> CreateAsync(ConditionDTO conditionDTO)
        {
            var entity = mapper.Map<ConditionModel>(conditionDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new condition with code {ConditionCode}", created.ConditionCode);
            return mapper.Map<ConditionDTO>(created);
        }

        public async Task<ConditionDTO> UpdateAsync(ConditionDTO conditionDTO)
        {
            var entity = mapper.Map<ConditionModel>(conditionDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated condition with code {ConditionCode}", updated.ConditionCode);
            return mapper.Map<ConditionDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted condition with code {ConditionCode}", code);
        }
    }
}