using DAL.Configuration.Database;
using DAL.Models.Treatment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class TreatmentProductRepository(HerdsyncDBContext context, ILogger<TreatmentProductRepository> logger) : ITreatmentProductRepository
    {
        public async Task<IEnumerable<TreatmentProductModel>> GetAllAsync()
        {
            return await context.TreatmentProducts
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<TreatmentProductModel?> GetByIdAsync(Guid treatmentProductId)
        {
            return await context.TreatmentProducts
                .FirstOrDefaultAsync(t => t.TreatmentProductId == treatmentProductId && !t.IsDeleted);
        }

        public async Task<TreatmentProductModel> AddAsync(TreatmentProductModel treatmentProduct)
        {
            context.TreatmentProducts.Add(treatmentProduct);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new treatment product with ID {TreatmentProductId}", treatmentProduct.TreatmentProductId);
            return treatmentProduct;
        }

        public async Task<TreatmentProductModel> UpdateAsync(TreatmentProductModel treatmentProduct)
        {
            context.TreatmentProducts.Update(treatmentProduct);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated treatment product with ID {TreatmentProductId}", treatmentProduct.TreatmentProductId);
            return treatmentProduct;
        }

        public async Task SoftDeleteAsync(Guid treatmentProductId)
        {
            var entity = await context.TreatmentProducts.FirstOrDefaultAsync(t => t.TreatmentProductId == treatmentProductId);
            if (entity == null) throw new KeyNotFoundException($"TreatmentProduct {treatmentProductId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted treatment product with ID {TreatmentProductId}", treatmentProductId);
        }
    }

}