using DAL.Models.Program.ProgramRun;

namespace DAL.Repositories
{
    public interface IProgramRunObservationRepository
    {
        Task<IEnumerable<ProgramRunObservationModel>> GetAllAsync();

        Task<ProgramRunObservationModel?> GetByIdAsync(Guid programRunObservationId);

        Task<ProgramRunObservationModel> AddAsync(ProgramRunObservationModel programRunObservation);

        Task<ProgramRunObservationModel> UpdateAsync(ProgramRunObservationModel programRunObservation);

        Task SoftDeleteAsync(Guid programRunObservationId);
    }
}