using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramTemplateRuleTreatmentService
    {
        Task<IEnumerable<ProgramTemplateRuleTreatmentDTO>> GetAllAsync();

        Task<ProgramTemplateRuleTreatmentDTO?> GetByIdAsync(Guid programTemplateRuleTreatmentId);

        Task<ProgramTemplateRuleTreatmentDTO> CreateAsync(ProgramTemplateRuleTreatmentDTO programTemplateRuleTreatmentDTO);

        Task<ProgramTemplateRuleTreatmentDTO> UpdateAsync(ProgramTemplateRuleTreatmentDTO programTemplateRuleTreatmentDTO);

        Task SoftDeleteAsync(Guid programTemplateRuleTreatmentId);
    }
}