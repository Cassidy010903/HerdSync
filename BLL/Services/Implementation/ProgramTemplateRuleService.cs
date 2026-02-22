using AutoMapper;
using DAL.Models.Program.ProgramTemplate;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class ProgramTemplateRuleService(IMapper mapper, IProgramTemplateRuleRepository repository, ILogger<ProgramTemplateRuleService> logger) : IProgramTemplateRuleService
    {
        public async Task<IEnumerable<ProgramTemplateRuleDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProgramTemplateRuleDTO>>(entities);
        }

        public async Task<ProgramTemplateRuleDTO?> GetByIdAsync(Guid programTemplateRuleId)
        {
            var entity = await repository.GetByIdAsync(programTemplateRuleId);
            return entity == null ? null : mapper.Map<ProgramTemplateRuleDTO>(entity);
        }

        public async Task<ProgramTemplateRuleDTO> CreateAsync(ProgramTemplateRuleDTO programTemplateRuleDTO)
        {
            var entity = mapper.Map<ProgramTemplateRuleModel>(programTemplateRuleDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program template rule with ID {ProgramTemplateRuleId}", created.ProgramTemplateRuleId);
            return mapper.Map<ProgramTemplateRuleDTO>(created);
        }

        public async Task<ProgramTemplateRuleDTO> UpdateAsync(ProgramTemplateRuleDTO programTemplateRuleDTO)
        {
            var entity = mapper.Map<ProgramTemplateRuleModel>(programTemplateRuleDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program template rule with ID {ProgramTemplateRuleId}", updated.ProgramTemplateRuleId);
            return mapper.Map<ProgramTemplateRuleDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid programTemplateRuleId)
        {
            await repository.SoftDeleteAsync(programTemplateRuleId);
            logger.LogInformation("Soft deleted program template rule with ID {ProgramTemplateRuleId}", programTemplateRuleId);
        }
    }
}