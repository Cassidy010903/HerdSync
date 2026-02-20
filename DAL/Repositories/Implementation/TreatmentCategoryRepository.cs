using DAL.Configuration.Database;
using DAL.Models.Treatment;

namespace DAL.Repositories.Implementation
{
    public class TreatmentCategoryRepository(HerdSyncDbContext context, ILogger<TreatmentCategoryRepository> logger) : ITreatmentCategoryRepository
    {
        public async Task<IEnumerable<TreatmentCategoryModel>> GetAllAsync()
        {
            return await context.TreatmentCategories
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<TreatmentCategoryModel?> GetByCodeAsync(string code)
        {
            return await context.TreatmentCategories
                .FirstOrDefaultAsync(t => t.CategoryName == code && !t.IsDeleted);
        }

        public async Task<TreatmentCategoryModel> AddAsync(TreatmentCategoryModel treatmentCategory)
        {
            context.TreatmentCategories.Add(treatmentCategory);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new treatment category with name {CategoryName}", treatmentCategory.CategoryName);
            return treatmentCategory;
        }

        public async Task<TreatmentCategoryModel> UpdateAsync(TreatmentCategoryModel treatmentCategory)
        {
            context.TreatmentCategories.Update(treatmentCategory);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated treatment category with name {CategoryName}", treatmentCategory.CategoryName);
            return treatmentCategory;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.TreatmentCategories.FirstOrDefaultAsync(t => t.CategoryName == code);
            if (entity == null) throw new KeyNotFoundException($"TreatmentCategory '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted treatment category with name {CategoryName}", code);
        }
    }
}