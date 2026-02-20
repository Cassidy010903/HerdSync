using DAL.Models;

namespace BLL.Services
{
    public interface IProgramService
    {
        Task<List<prg_Program>> ListAsync();

        Task<prg_Program?> GetAsync(Guid programId);
    }
}