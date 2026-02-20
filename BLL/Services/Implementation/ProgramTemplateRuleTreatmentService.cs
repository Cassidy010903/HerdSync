using DAL.Models.Program.ProgramTemplate;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;

namespace BLL.Services.Implementation
{
    public class ProgramTemplateRuleTreatmentService(IMapper mapper, IProgramTemplateRuleTreatmentRepository repository, ILogger<ProgramTemplateRuleTreatmentService> logger) : IProgramTemplateRuleTreatmentService
    {
        public async Task<IEnumerable<ProgramTemplateRuleTreatmentDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProgramTemplateRuleTreatmentDTO>>(entities);
        }

        public async Task<ProgramTemplateRuleTreatmentDTO?> GetByIdAsync(Guid programTemplateRuleTreatmentId)
        {
            var entity = await repository.GetByIdAsync(programTemplateRuleTreatmentId);
            return entity == null ? null : mapper.Map<ProgramTemplateRuleTreatmentDTO>(entity);
        }

        public async Task<ProgramTemplateRuleTreatmentDTO> CreateAsync(ProgramTemplateRuleTreatmentDTO programTemplateRuleTreatmentDTO)
        {
            var entity = mapper.Map<ProgramTemplateRuleTreatmentModel>(programTemplateRuleTreatmentDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program template rule treatment with ID {ProgramTemplateRuleTreatmentId}", created.ProgramTemplateRuleTreatmentId);
            return mapper.Map<ProgramTemplateRuleTreatmentDTO>(created);
        }

        public async Task<ProgramTemplateRuleTreatmentDTO> UpdateAsync(ProgramTemplateRuleTreatmentDTO programTemplateRuleTreatmentDTO)
        {
            var entity = mapper.Map<ProgramTemplateRuleTreatmentModel>(programTemplateRuleTreatmentDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program template rule treatment with ID {ProgramTemplateRuleTreatmentId}", updated.ProgramTemplateRuleTreatmentId);
            return mapper.Map<ProgramTemplateRuleTreatmentDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid programTemplateRuleTreatmentId)
        {
            await repository.SoftDeleteAsync(programTemplateRuleTreatmentId);
            logger.LogInformation("Soft deleted program template rule treatment with ID {ProgramTemplateRuleTreatmentId}", programTemplateRuleTreatmentId);
        }
    }
}