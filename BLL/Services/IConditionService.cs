using HerdSync.Shared.DTO.Treatment;

namespace BLL.Services
{
    public interface IConditionService
    {
        Task<IEnumerable<ConditionDTO>> GetAllAsync();

        Task<ConditionDTO?> GetBycodeAsync(string code);

        Task<ConditionDTO> CreateAsync(ConditionDTO conditionDTO);

        Task<ConditionDTO> UpdateAsync(ConditionDTO conditionDTO);

        Task SoftDeleteAsync(string code);
    }
}