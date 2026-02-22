using DAL.Configuration.Database;
using DAL.Models.Program.ProgramTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class ProgramTemplateRepository(HerdsyncDBContext context, ILogger<ProgramTemplateRepository> logger) : IProgramTemplateRepository
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
            context.ProgramTemplates.Add(programTemplate);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program template with code {Code}", programTemplate.ProgramTemplateCode);
            return programTemplate;
        }

        public async Task<ProgramTemplateModel> UpdateAsync(ProgramTemplateModel programTemplate)
        {
            context.ProgramTemplates.Update(programTemplate);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program template with code {Code}", programTemplate.ProgramTemplateCode);
            return programTemplate;
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