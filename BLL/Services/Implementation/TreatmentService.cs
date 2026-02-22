using AutoMapper;
using DAL.Models.Treatment;
using DAL.Repositories;
using HerdSync.Shared.DTO.Treatment;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class TreatmentService(IMapper mapper, ITreatmentRepository repository, ILogger<TreatmentService> logger) : ITreatmentService
    {
        public async Task<IEnumerable<TreatmentDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<TreatmentDTO>>(entities);
        }

        public async Task<TreatmentDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<TreatmentDTO>(entity);
        }

        public async Task<TreatmentDTO> CreateAsync(TreatmentDTO treatmentDTO)
        {
            var entity = mapper.Map<TreatmentModel>(treatmentDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new treatment with code {TreatmentCode}", created.TreatmentCode);
            return mapper.Map<TreatmentDTO>(created);
        }

        public async Task<TreatmentDTO> UpdateAsync(TreatmentDTO treatmentDTO)
        {
            var entity = mapper.Map<TreatmentModel>(treatmentDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated treatment with code {TreatmentCode}", updated.TreatmentCode);
            return mapper.Map<TreatmentDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted treatment with code {TreatmentCode}", code);
        }
    }
}