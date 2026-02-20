using DAL.Models.Animal;

namespace DAL.Repositories
{
    public interface IAnimalEventTypeRepository
    {
        Task<IEnumerable<AnimalEventTypeModel>> GetAllAsync();
        Task<AnimalEventTypeModel?> GetByCodeAsync(string code);
        Task<AnimalEventTypeModel> AddAsync(AnimalEventTypeModel animalEventType);
        Task<AnimalEventTypeModel> UpdateAsync(AnimalEventTypeModel animalEventType);
        Task SoftDeleteAsync(string code);
    }
}