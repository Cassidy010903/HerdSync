using DAL.Models.Animal;

namespace DAL.Repositories
{
    public interface IAnimalTypeRepository
    {
        Task<IEnumerable<AnimalTypeModel>> GetAllAsync();

        Task<AnimalTypeModel?> GetByCodeAsync(string code);

        Task<AnimalTypeModel> AddAsync(AnimalTypeModel animalType);

        Task<AnimalTypeModel> UpdateAsync(AnimalTypeModel animalType);

        Task SoftDeleteAsync(string code);
    }
}