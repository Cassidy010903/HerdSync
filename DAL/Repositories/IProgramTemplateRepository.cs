using DAL.Models.Program.ProgramTemplate;

namespace DAL.Repositories
{
    public interface IProgramTemplateRepository
    {
        Task<IEnumerable<ProgramTemplateModel>> GetAllAsync();
        Task<ProgramTemplateModel?> GetByCodeAsync(string code);
        Task<ProgramTemplateModel> AddAsync(ProgramTemplateModel programTemplate);
        Task<ProgramTemplateModel> UpdateAsync(ProgramTemplateModel programTemplate);
        Task SoftDeleteAsync(string code);
    }
}