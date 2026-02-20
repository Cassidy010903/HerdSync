using DAL.Models.Program.ProgramTemplate;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;

namespace BLL.Services.Implementation
{
    public class ProgramTemplateService(IMapper mapper, IProgramTemplateRepository repository, ILogger<ProgramTemplateService> logger) : IProgramTemplateService
    {
        public async Task<IEnumerable<ProgramTemplateDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProgramTemplateDTO>>(entities);
        }

        public async Task<ProgramTemplateDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<ProgramTemplateDTO>(entity);
        }

        public async Task<ProgramTemplateDTO> CreateAsync(ProgramTemplateDTO programTemplateDTO)
        {
            var entity = mapper.Map<ProgramTemplateModel>(programTemplateDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program template with code {Code}", created.ProgramTemplateCode);
            return mapper.Map<ProgramTemplateDTO>(created);
        }

        public async Task<ProgramTemplateDTO> UpdateAsync(ProgramTemplateDTO programTemplateDTO)
        {
            var entity = mapper.Map<ProgramTemplateModel>(programTemplateDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program template with code {Code}", updated.ProgramTemplateCode);
            return mapper.Map<ProgramTemplateDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted program template with code {Code}", code);
        }
    }
}