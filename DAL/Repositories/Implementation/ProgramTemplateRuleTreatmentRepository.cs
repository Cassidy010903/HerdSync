using DAL.Configuration.Database;
using DAL.Models.Program.ProgramTemplate;

namespace DAL.Repositories.Implementation
{
    public class ProgramTemplateRuleTreatmentRepository(HerdSyncDbContext context, ILogger<ProgramTemplateRuleTreatmentRepository> logger) : IProgramTemplateRuleTreatmentRepository
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
            context.ProgramTemplateRuleTreatments.Add(programTemplateRuleTreatment);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program template rule treatment with ID {ProgramTemplateRuleTreatmentId}", programTemplateRuleTreatment.ProgramTemplateRuleTreatmentId);
            return programTemplateRuleTreatment;
        }

        public async Task<ProgramTemplateRuleTreatmentModel> UpdateAsync(ProgramTemplateRuleTreatmentModel programTemplateRuleTreatment)
        {
            context.ProgramTemplateRuleTreatments.Update(programTemplateRuleTreatment);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program template rule treatment with ID {ProgramTemplateRuleTreatmentId}", programTemplateRuleTreatment.ProgramTemplateRuleTreatmentId);
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