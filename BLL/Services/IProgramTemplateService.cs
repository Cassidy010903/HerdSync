using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramTemplateService
    {
        Task<IEnumerable<ProgramTemplateDTO>> GetAllAsync();

        Task<ProgramTemplateDTO?> GetByIdAsync(string code);

        Task<ProgramTemplateDTO> CreateAsync(ProgramTemplateDTO programTemplateDTO);

        Task<ProgramTemplateDTO> UpdateAsync(ProgramTemplateDTO programTemplateDTO);

        Task SoftDeleteAsync(string code);
    }
}