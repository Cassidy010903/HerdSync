using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IAnimalService
    {
        public Task AddAnimalAsync(AnimalDTO animalDTO);

        Task<List<AnimalDTO>> GetAllHerdAsync();

        public Task UpdateAnimalAsync(AnimalDTO animalDTO);
    }
    public interface IAnimalService2
    {
        Task<IEnumerable<AnimalDTO>> GetAllAsync();
        Task<AnimalDTO?> GetByIdAsync(Guid animalId);
        Task<AnimalDTO> CreateAsync(AnimalDTO animalDTO);
        Task<AnimalDTO> UpdateAsync(AnimalDTO animalDTO);
        Task SoftDeleteAsync(Guid animalId);
    }
}