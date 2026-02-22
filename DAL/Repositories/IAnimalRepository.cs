using DAL.Models.Animal;

namespace DAL.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<AnimalModel>> GetAllAsync();
        Task<AnimalModel?> GetByIdAsync(Guid animalId);
        Task<AnimalModel> AddAsync(AnimalModel animal);
        Task<AnimalModel> UpdateAsync(AnimalModel animal);
        Task SoftDeleteAsync(Guid animalId);
    }
}