using DAL.Models.Animal;

namespace DAL.Repositories
{
    public interface IPregnancyRepository
    {
        Task<IEnumerable<PregnancyModel>> GetAllAsync();

        Task<PregnancyModel?> GetByIdAsync(Guid pregnancyId);

        Task<PregnancyModel> AddAsync(PregnancyModel pregnancy);

        Task<PregnancyModel> UpdateAsync(PregnancyModel pregnancy);

        Task SoftDeleteAsync(Guid pregnancyId);
    }
}