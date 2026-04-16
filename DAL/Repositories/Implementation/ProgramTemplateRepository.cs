using DAL.Configuration.Database;
using DAL.Models.Program.ProgramTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class ProgramTemplateRepository(KuddeDBContext context, ILogger<ProgramTemplateRepository> logger) : IProgramTemplateRepository
    {
        public async Task<IEnumerable<ProgramTemplateModel>> GetAllAsync()
        {
            return await context.ProgramTemplates
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramTemplateModel?> GetByCodeAsync(string code)
        {
            return await context.ProgramTemplates
                .FirstOrDefaultAsync(p => p.ProgramTemplateCode == code && !p.IsDeleted);
        }

        public async Task<ProgramTemplateModel> AddAsync(ProgramTemplateModel programTemplate)
        {
            var local = context.ProgramTemplates.Local
                .FirstOrDefault(p => p.ProgramTemplateCode == programTemplate.ProgramTemplateCode);
            if (local != null) context.Entry(local).State = EntityState.Detached;

            context.Entry(programTemplate).State = EntityState.Added;
            await context.SaveChangesAsync();
            return programTemplate;
        }

        public async Task<ProgramTemplateModel> UpdateAsync(ProgramTemplateModel programTemplate)
        {
            var existing = await context.ProgramTemplates
                .FirstOrDefaultAsync(p => p.ProgramTemplateCode == programTemplate.ProgramTemplateCode);
            if (existing == null)
                throw new KeyNotFoundException($"ProgramTemplate '{programTemplate.ProgramTemplateCode}' not found.");

            existing.TemplateName = programTemplate.TemplateName;
            existing.Description = programTemplate.Description;
            existing.IsActive = programTemplate.IsActive;
            existing.Frequency = programTemplate.Frequency;

            await context.SaveChangesAsync();
            logger.LogInformation("Updated program template with code {Code}", existing.ProgramTemplateCode);
            return existing;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.ProgramTemplates.FirstOrDefaultAsync(p => p.ProgramTemplateCode == code);
            if (entity == null) throw new KeyNotFoundException($"ProgramTemplate '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program template with code {Code}", code);
        }
    }
}