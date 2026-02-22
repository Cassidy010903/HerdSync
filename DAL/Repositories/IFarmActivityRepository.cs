using DAL.Models.Farm;

namespace DAL.Repositories
{
    public interface IFarmActivityRepository
    {
        Task<IEnumerable<FarmActivityModel>> GetAllAsync();

        Task<FarmActivityModel?> GetByIdAsync(Guid farmActivityId);

        Task<FarmActivityModel> AddAsync(FarmActivityModel farmActivity);

        Task<FarmActivityModel> UpdateAsync(FarmActivityModel farmActivity);

        Task SoftDeleteAsync(Guid farmActivityId);
    }
}