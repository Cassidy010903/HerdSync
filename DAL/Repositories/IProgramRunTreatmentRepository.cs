using DAL.Models.Program.ProgramRun;

namespace DAL.Repositories
{
    public interface IProgramRunTreatmentRepository
    {
        Task<IEnumerable<ProgramRunTreatmentModel>> GetAllAsync();

        Task<ProgramRunTreatmentModel?> GetByIdAsync(Guid programRunTreatmentId);

        Task<ProgramRunTreatmentModel> AddAsync(ProgramRunTreatmentModel programRunTreatment);

        Task<ProgramRunTreatmentModel> UpdateAsync(ProgramRunTreatmentModel programRunTreatment);

        Task SoftDeleteAsync(Guid programRunTreatmentId);
    }
}