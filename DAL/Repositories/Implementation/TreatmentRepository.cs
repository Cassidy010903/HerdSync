using DAL.Configuration.Database;
using DAL.Models.Treatment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class TreatmentRepository(HerdsyncDBContext context, ILogger<TreatmentRepository> logger) : ITreatmentRepository
    {
        public async Task<IEnumerable<TreatmentModel>> GetAllAsync()
        {
            return await context.Treatments
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<TreatmentModel?> GetByCodeAsync(string code)
        {
            return await context.Treatments
                .FirstOrDefaultAsync(t => t.TreatmentCode == code && !t.IsDeleted);
        }

        public async Task<TreatmentModel> AddAsync(TreatmentModel treatment)
        {
            context.Treatments.Add(treatment);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new treatment with code {TreatmentCode}", treatment.TreatmentCode);
            return treatment;
        }

        public async Task<TreatmentModel> UpdateAsync(TreatmentModel treatment)
        {
            context.Treatments.Update(treatment);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated treatment with code {TreatmentCode}", treatment.TreatmentCode);
            return treatment;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.Treatments.FirstOrDefaultAsync(t => t.TreatmentCode == code);
            if (entity == null) throw new KeyNotFoundException($"Treatment '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted treatment with code {TreatmentCode}", code);
        }
    }
}