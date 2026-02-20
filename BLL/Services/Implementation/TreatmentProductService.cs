using DAL.Models.Treatment;
using DAL.Repositories;
using HerdSync.Shared.DTO.Treatment;

namespace BLL.Services.Implementation
{
    public class TreatmentProductService(IMapper mapper, ITreatmentProductRepository repository, ILogger<TreatmentProductService> logger) : ITreatmentProductService
    {
        public async Task<IEnumerable<TreatmentProductDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<TreatmentProductDTO>>(entities);
        }

        public async Task<TreatmentProductDTO?> GetByIdAsync(Guid treatmentProductId)
        {
            var entity = await repository.GetByIdAsync(treatmentProductId);
            return entity == null ? null : mapper.Map<TreatmentProductDTO>(entity);
        }

        public async Task<TreatmentProductDTO> CreateAsync(TreatmentProductDTO treatmentProductDTO)
        {
            var entity = mapper.Map<TreatmentProductModel>(treatmentProductDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new treatment product with ID {TreatmentProductId}", created.TreatmentProductId);
            return mapper.Map<TreatmentProductDTO>(created);
        }

        public async Task<TreatmentProductDTO> UpdateAsync(TreatmentProductDTO treatmentProductDTO)
        {
            var entity = mapper.Map<TreatmentProductModel>(treatmentProductDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated treatment product with ID {TreatmentProductId}", updated.TreatmentProductId);
            return mapper.Map<TreatmentProductDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid treatmentProductId)
        {
            await repository.SoftDeleteAsync(treatmentProductId);
            logger.LogInformation("Soft deleted treatment product with ID {TreatmentProductId}", treatmentProductId);
        }
    }
}