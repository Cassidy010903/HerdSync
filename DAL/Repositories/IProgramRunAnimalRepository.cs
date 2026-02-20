using DAL.Models.Program.ProgramRun;

namespace DAL.Repositories
{
    public interface IProgramRunAnimalRepository
    {
        Task<IEnumerable<ProgramRunAnimalModel>> GetAllAsync();
        Task<ProgramRunAnimalModel?> GetByIdAsync(Guid programRunAnimalId);
        Task<ProgramRunAnimalModel> AddAsync(ProgramRunAnimalModel programRunAnimal);
        Task<ProgramRunAnimalModel> UpdateAsync(ProgramRunAnimalModel programRunAnimal);
        Task SoftDeleteAsync(Guid programRunAnimalId);
    }
}