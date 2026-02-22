using DAL.Configuration.Database;
using DAL.Models.Program.ProgramTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class ProgramTemplateRuleRepository(HerdsyncDBContext context, ILogger<ProgramTemplateRuleRepository> logger) : IProgramTemplateRuleRepository
    {
        public async Task<IEnumerable<ProgramTemplateRuleModel>> GetAllAsync()
        {
            return await context.ProgramTemplateRules
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProgramTemplateRuleModel?> GetByIdAsync(Guid programTemplateRuleId)
        {
            return await context.ProgramTemplateRules
                .FirstOrDefaultAsync(p => p.ProgramTemplateRuleId == programTemplateRuleId && !p.IsDeleted);
        }

        public async Task<ProgramTemplateRuleModel> AddAsync(ProgramTemplateRuleModel programTemplateRule)
        {
            context.ProgramTemplateRules.Add(programTemplateRule);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new program template rule with ID {ProgramTemplateRuleId}", programTemplateRule.ProgramTemplateRuleId);
            return programTemplateRule;
        }

        public async Task<ProgramTemplateRuleModel> UpdateAsync(ProgramTemplateRuleModel programTemplateRule)
        {
            context.ProgramTemplateRules.Update(programTemplateRule);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated program template rule with ID {ProgramTemplateRuleId}", programTemplateRule.ProgramTemplateRuleId);
            return programTemplateRule;
        }

        public async Task SoftDeleteAsync(Guid programTemplateRuleId)
        {
            var entity = await context.ProgramTemplateRules.FirstOrDefaultAsync(p => p.ProgramTemplateRuleId == programTemplateRuleId);
            if (entity == null) throw new KeyNotFoundException($"ProgramTemplateRule {programTemplateRuleId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted program template rule with ID {ProgramTemplateRuleId}", programTemplateRuleId);
        }
    }
}