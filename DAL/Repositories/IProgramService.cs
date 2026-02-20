using DAL.Models;

namespace DAL.Repositories
{
    public interface IProgramService
    {
        Task<List<prg_Program>> ListAsync();

        Task<prg_Program?> GetAsync(Guid programId);
    }
}