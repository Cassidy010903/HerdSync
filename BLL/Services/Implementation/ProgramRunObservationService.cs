using DAL.Models.Program.ProgramRun;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;

namespace BLL.Services.Implementation
{
    public class ProgramRunObservationService(IMapper mapper, IProgramRunObservationRepository repository, ILogger<ProgramRunObservationService> logger) : IProgramRunObservationService
    {
        public async Task<IEnumerable<ProgramRunObservationDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProgramRunObservationDTO>>(entities);
        }

        public async Task<ProgramRunObservationDTO?> GetByIdAsync(Guid programRunObservationId)
        {
            var entity = await repository.GetByIdAsync(programRunObservationId);
            return entity == null ? null : mapper.Map<ProgramRunObservationDTO>(entity);
        }

        public async Task<ProgramRunObservationDTO> CreateAsync(ProgramRunObservationDTO programRunObservationDTO)
        {
            var entity = mapper.Map<ProgramRunObservationModel>(programRunObservationDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program run observation with ID {ProgramRunObservationId}", created.ProgramRunObservationId);
            return mapper.Map<ProgramRunObservationDTO>(created);
        }

        public async Task<ProgramRunObservationDTO> UpdateAsync(ProgramRunObservationDTO programRunObservationDTO)
        {
            var entity = mapper.Map<ProgramRunObservationModel>(programRunObservationDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program run observation with ID {ProgramRunObservationId}", updated.ProgramRunObservationId);
            return mapper.Map<ProgramRunObservationDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid programRunObservationId)
        {
            await repository.SoftDeleteAsync(programRunObservationId);
            logger.LogInformation("Soft deleted program run observation with ID {ProgramRunObservationId}", programRunObservationId);
        }
    }
}