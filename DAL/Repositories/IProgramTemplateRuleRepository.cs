using DAL.Models.Program.ProgramTemplate;

namespace DAL.Repositories
{
    public interface IProgramTemplateRuleRepository
    {
        Task<IEnumerable<ProgramTemplateRuleModel>> GetAllAsync();
        Task<ProgramTemplateRuleModel?> GetByIdAsync(Guid programTemplateRuleId);
        Task<ProgramTemplateRuleModel> AddAsync(ProgramTemplateRuleModel programTemplateRule);
        Task<ProgramTemplateRuleModel> UpdateAsync(ProgramTemplateRuleModel programTemplateRule);
        Task SoftDeleteAsync(Guid programTemplateRuleId);
    }
}