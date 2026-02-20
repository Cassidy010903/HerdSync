using DAL.Models.Program.ProgramTemplate;

namespace DAL.Repositories
{
    public interface IProgramTemplateRuleTreatmentRepository
    {
        Task<IEnumerable<ProgramTemplateRuleTreatmentModel>> GetAllAsync();
        Task<ProgramTemplateRuleTreatmentModel?> GetByIdAsync(Guid programTemplateRuleTreatmentId);
        Task<ProgramTemplateRuleTreatmentModel> AddAsync(ProgramTemplateRuleTreatmentModel programTemplateRuleTreatment);
        Task<ProgramTemplateRuleTreatmentModel> UpdateAsync(ProgramTemplateRuleTreatmentModel programTemplateRuleTreatment);
        Task SoftDeleteAsync(Guid programTemplateRuleTreatmentId);
    }
}