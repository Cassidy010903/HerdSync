using HerdSync.Shared.DTO.Program;

namespace BLL.Services
{
    public interface IProgramRunAnimalService
    {
        Task<IEnumerable<ProgramRunAnimalDTO>> GetAllAsync();

        Task<ProgramRunAnimalDTO?> GetByIdAsync(Guid programRunAnimalId);

        Task<ProgramRunAnimalDTO> CreateAsync(ProgramRunAnimalDTO programRunAnimalDTO);

        Task<ProgramRunAnimalDTO> UpdateAsync(ProgramRunAnimalDTO programRunAnimalDTO);

        Task SoftDeleteAsync(Guid programRunAnimalId);
    }
}