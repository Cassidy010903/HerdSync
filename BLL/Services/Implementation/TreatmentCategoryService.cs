using DAL.Models.Treatment;
using DAL.Repositories;
using HerdSync.Shared.DTO.Treatment;

namespace BLL.Services.Implementation
{
    public class TreatmentCategoryService(IMapper mapper, ITreatmentCategoryRepository repository, ILogger<TreatmentCategoryService> logger) : ITreatmentCategoryService
    {
        public async Task<IEnumerable<TreatmentCategoryDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<TreatmentCategoryDTO>>(entities);
        }

        public async Task<TreatmentCategoryDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<TreatmentCategoryDTO>(entity);
        }

        public async Task<TreatmentCategoryDTO> CreateAsync(TreatmentCategoryDTO treatmentCategoryDTO)
        {
            var entity = mapper.Map<TreatmentCategoryModel>(treatmentCategoryDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new treatment category with name {CategoryName}", created.CategoryName);
            return mapper.Map<TreatmentCategoryDTO>(created);
        }

        public async Task<TreatmentCategoryDTO> UpdateAsync(TreatmentCategoryDTO treatmentCategoryDTO)
        {
            var entity = mapper.Map<TreatmentCategoryModel>(treatmentCategoryDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated treatment category with name {CategoryName}", updated.CategoryName);
            return mapper.Map<TreatmentCategoryDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted treatment category with name {CategoryName}", code);
        }
    }
}