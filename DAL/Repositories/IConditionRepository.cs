using DAL.Models.Treatment;

namespace DAL.Repositories
{
    public interface IConditionRepository
    {
        Task<IEnumerable<ConditionModel>> GetAllAsync();
        Task<ConditionModel?> GetByCodeAsync(string code);
        Task<ConditionModel> AddAsync(ConditionModel condition);
        Task<ConditionModel> UpdateAsync(ConditionModel condition);
        Task SoftDeleteAsync(string code);
    }
}