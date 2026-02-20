using DAL.Models.Program.ProgramRun;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;

namespace BLL.Services.Implementation
{
    public class ProgramRunAnimalService(IMapper mapper, IProgramRunAnimalRepository repository, ILogger<ProgramRunAnimalService> logger) : IProgramRunAnimalService
    {
        public async Task<IEnumerable<ProgramRunAnimalDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<ProgramRunAnimalDTO>>(entities);
        }

        public async Task<ProgramRunAnimalDTO?> GetByIdAsync(Guid programRunAnimalId)
        {
            var entity = await repository.GetByIdAsync(programRunAnimalId);
            return entity == null ? null : mapper.Map<ProgramRunAnimalDTO>(entity);
        }

        public async Task<ProgramRunAnimalDTO> CreateAsync(ProgramRunAnimalDTO programRunAnimalDTO)
        {
            var entity = mapper.Map<ProgramRunAnimalModel>(programRunAnimalDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program run animal with ID {ProgramRunAnimalId}", created.ProgramRunAnimalId);
            return mapper.Map<ProgramRunAnimalDTO>(created);
        }

        public async Task<ProgramRunAnimalDTO> UpdateAsync(ProgramRunAnimalDTO programRunAnimalDTO)
        {
            var entity = mapper.Map<ProgramRunAnimalModel>(programRunAnimalDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program run animal with ID {ProgramRunAnimalId}", updated.ProgramRunAnimalId);
            return mapper.Map<ProgramRunAnimalDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid programRunAnimalId)
        {
            await repository.SoftDeleteAsync(programRunAnimalId);
            logger.LogInformation("Soft deleted program run animal with ID {ProgramRunAnimalId}", programRunAnimalId);
        }
    }
}