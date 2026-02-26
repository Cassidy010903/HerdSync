using DAL.Configuration.Database;
using DAL.Models.Program.ProgramTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class ProgramTemplateRuleTreatmentRepository(HerdsyncDBContext context, ILogger<ProgramTemplateRuleTreatmentRepository> logger) : IProgramTemplateRuleTreatmentRepository
    {
        public async Task<IEnumerable<ProgramTemplateRuleTreatmentModel>> GetAllAsync()
        {
            return await context.ProgramTemplateRuleTreatments
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramTemplateRuleTreatmentModel?> GetByIdAsync(Guid programTemplateRuleTreatmentId)
        {
            return await context.ProgramTemplateRuleTreatments
                .FirstOrDefaultAsync(p => p.ProgramTemplateRuleTreatmentId == programTemplateRuleTreatmentId && !p.IsDeleted);
        }

        public async Task<ProgramTemplateRuleTreatmentModel> AddAsync(ProgramTemplateRuleTreatmentModel programTemplateRuleTreatment)
        {
            var local = context.ProgramTemplateRuleTreatments.Local
                .FirstOrDefault(p => p.ProgramTemplateRuleTreatmentId == programTemplateRuleTreatment.ProgramTemplateRuleTreatmentId);
            if (local != null) context.Entry(local).State = EntityState.Detached;

            context.Entry(programTemplateRuleTreatment).State = EntityState.Added;
            await context.SaveChangesAsync();
            return programTemplateRuleTreatment;
        }

        public async Task<ProgramTemplateRuleTreatmentModel> UpdateAsync(ProgramTemplateRuleTreatmentModel programTemplateRuleTreatment)
        {
            var local = context.ProgramTemplateRuleTreatments.Local
                .FirstOrDefault(p => p.ProgramTemplateRuleTreatmentId == programTemplateRuleTreatment.ProgramTemplateRuleTreatmentId);
            if (local != null) context.Entry(local).State = EntityState.Detached;

            context.Entry(programTemplateRuleTreatment).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return programTemplateRuleTreatment;
        }

        public async Task SoftDeleteAsync(Guid programTemplateRuleTreatmentId)
        {
            var entity = await context.ProgramTemplateRuleTreatments.FirstOrDefaultAsync(p => p.ProgramTemplateRuleTreatmentId == programTemplateRuleTreatmentId);
            if (entity == null) throw new KeyNotFoundException($"ProgramTemplateRuleTreatment {programTemplateRuleTreatmentId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program template rule treatment with ID {ProgramTemplateRuleTreatmentId}", programTemplateRuleTreatmentId);
        }
    }
}