using DAL.Models.Farm;

namespace DAL.Repositories
{
    public interface IFarmActivityTypeRepository
    {
        Task<IEnumerable<FarmActivityTypeModel>> GetAllAsync();
        Task<FarmActivityTypeModel?> GetByCodeAsync(string code);
        Task<FarmActivityTypeModel> AddAsync(FarmActivityTypeModel farmActivityType);
        Task<FarmActivityTypeModel> UpdateAsync(FarmActivityTypeModel farmActivityType);
        Task SoftDeleteAsync(string code);
    }
}