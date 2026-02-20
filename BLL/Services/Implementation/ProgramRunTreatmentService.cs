using DAL.Models.Program.ProgramRun;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;

namespace BLL.Services.Implementation
{
    public class ProgramRunTreatmentService(IMapper mapper, IProgramRunTreatmentRepository repository, ILogger<ProgramRunTreatmentService> logger) : IProgramRunTreatmentService
    {
        public async Task<IEnumerable<ProgramRunTreatmentDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProgramRunTreatmentDTO>>(entities);
        }

        public async Task<ProgramRunTreatmentDTO?> GetByIdAsync(Guid programRunTreatmentId)
        {
            var entity = await repository.GetByIdAsync(programRunTreatmentId);
            return entity == null ? null : mapper.Map<ProgramRunTreatmentDTO>(entity);
        }

        public async Task<ProgramRunTreatmentDTO> CreateAsync(ProgramRunTreatmentDTO programRunTreatmentDTO)
        {
            var entity = mapper.Map<ProgramRunTreatmentModel>(programRunTreatmentDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program run treatment with ID {ProgramRunTreatmentId}", created.ProgramRunTreatmentId);
            return mapper.Map<ProgramRunTreatmentDTO>(created);
        }

        public async Task<ProgramRunTreatmentDTO> UpdateAsync(ProgramRunTreatmentDTO programRunTreatmentDTO)
        {
            var entity = mapper.Map<ProgramRunTreatmentModel>(programRunTreatmentDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program run treatment with ID {ProgramRunTreatmentId}", updated.ProgramRunTreatmentId);
            return mapper.Map<ProgramRunTreatmentDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid programRunTreatmentId)
        {
            await repository.SoftDeleteAsync(programRunTreatmentId);
            logger.LogInformation("Soft deleted program run treatment with ID {ProgramRunTreatmentId}", programRunTreatmentId);
        }
    }
}