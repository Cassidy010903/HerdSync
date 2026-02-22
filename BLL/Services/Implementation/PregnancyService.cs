using AutoMapper;
using DAL.Models.Animal;
using DAL.Repositories;
using HerdSync.Shared.DTO.Animal;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class PregnancyService(IMapper mapper, IPregnancyRepository repository, ILogger<PregnancyService> logger) : IPregnancyService
    {
        public async Task<IEnumerable<PregnancyDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<PregnancyDTO>>(entities);
        }

        public async Task<PregnancyDTO?> GetByIdAsync(Guid pregnancyId)
        {
            var entity = await repository.GetByIdAsync(pregnancyId);
            return entity == null ? null : mapper.Map<PregnancyDTO>(entity);
        }

        public async Task<PregnancyDTO> CreateAsync(PregnancyDTO pregnancyDTO)
        {
            var entity = mapper.Map<PregnancyModel>(pregnancyDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new pregnancy record with ID {PregnancyId}", created.PregnancyId);
            return mapper.Map<PregnancyDTO>(created);
        }

        public async Task<PregnancyDTO> UpdateAsync(PregnancyDTO pregnancyDTO)
        {
            var entity = mapper.Map<PregnancyModel>(pregnancyDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated pregnancy record with ID {PregnancyId}", updated.PregnancyId);
            return mapper.Map<PregnancyDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid pregnancyId)
        {
            await repository.SoftDeleteAsync(pregnancyId);
            logger.LogInformation("Soft deleted pregnancy record with ID {PregnancyId}", pregnancyId);
        }
    }
}