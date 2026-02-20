using DAL.Models.Farm;

namespace DAL.Repositories
{
    public interface IFarmRepository
    {
        Task<IEnumerable<FarmModel>> GetAllAsync();
        Task<FarmModel?> GetByIdAsync(Guid farmId);
        Task<FarmModel> AddAsync(FarmModel farm);
        Task<FarmModel> UpdateAsync(FarmModel farm);
        Task SoftDeleteAsync(Guid farmId);
    }
}