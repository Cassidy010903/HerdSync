using AutoMapper;
using DAL.Models.Program.ProgramRun;
using DAL.Repositories;
using HerdSync.Shared.DTO.Program;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class ProgramRunService(IMapper mapper, IProgramRunRepository repository, ILogger<ProgramRunService> logger) : IProgramRunService
    {
        public async Task<List<ProgramRunDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<List<ProgramRunDTO>>(entities);
        }

        public async Task<ProgramRunDTO?> GetByIdAsync(Guid programRunId)
        {
            var entity = await repository.GetByIdAsync(programRunId);
            return entity == null ? null : mapper.Map<ProgramRunDTO>(entity);
        }

        public async Task<ProgramRunDTO> CreateAsync(ProgramRunDTO programRunDTO)
        {
            var entity = mapper.Map<ProgramRunModel>(programRunDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new program run with ID {ProgramRunId}", created.ProgramRunId);
            return mapper.Map<ProgramRunDTO>(created);
        }

        public async Task<ProgramRunDTO> UpdateAsync(ProgramRunDTO programRunDTO)
        {
            var entity = mapper.Map<ProgramRunModel>(programRunDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated program run with ID {ProgramRunId}", updated.ProgramRunId);
            return mapper.Map<ProgramRunDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid programRunId)
        {
            await repository.SoftDeleteAsync(programRunId);
            logger.LogInformation("Soft deleted program run with ID {ProgramRunId}", programRunId);
        }
    }
}