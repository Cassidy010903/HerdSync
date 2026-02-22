using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramTemplateRuleService
    {
        Task<IEnumerable<ProgramTemplateRuleDTO>> GetAllAsync();

        Task<ProgramTemplateRuleDTO?> GetByIdAsync(Guid programTemplateRuleId);

        Task<ProgramTemplateRuleDTO> CreateAsync(ProgramTemplateRuleDTO programTemplateRuleDTO);

        Task<ProgramTemplateRuleDTO> UpdateAsync(ProgramTemplateRuleDTO programTemplateRuleDTO);

        Task SoftDeleteAsync(Guid programTemplateRuleId);
    }
}