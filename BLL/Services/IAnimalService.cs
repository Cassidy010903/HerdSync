using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IAnimalService
    {
        Task<List<AnimalDTO>> GetAllAsync();

        Task<AnimalDTO?> GetByIdAsync(Guid animalId);

        Task<AnimalDTO> CreateAsync(AnimalDTO animalDTO);

        Task<AnimalDTO> UpdateAsync(AnimalDTO animalDTO);

        Task SoftDeleteAsync(Guid animalId);
    }
}