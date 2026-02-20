using DAL.Models.Animal;

namespace DAL.Repositories
{
    public interface IAnimalTagRepository
    {
        Task<IEnumerable<AnimalTagModel>> GetAllAsync();
        Task<AnimalTagModel?> GetByIdAsync(Guid animalTagId);
        Task<AnimalTagModel> AddAsync(AnimalTagModel animalTag);
        Task<AnimalTagModel> UpdateAsync(AnimalTagModel animalTag);
        Task SoftDeleteAsync(Guid animalTagId);
    }
}