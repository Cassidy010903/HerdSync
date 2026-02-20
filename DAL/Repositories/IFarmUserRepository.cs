using DAL.Models.Authentication;

namespace DAL.Repositories
{
    public interface IFarmUserRepository
    {
        Task<IEnumerable<FarmUserModel>> GetAllAsync();
        Task<FarmUserModel?> GetByIdAsync(Guid farmUserId);
        Task<FarmUserModel> AddAsync(FarmUserModel farmUser);
        Task<FarmUserModel> UpdateAsync(FarmUserModel farmUser);
        Task SoftDeleteAsync(Guid farmUserId);
    }
}