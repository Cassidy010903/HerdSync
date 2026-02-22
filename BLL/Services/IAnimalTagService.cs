using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IAnimalTagService
    {
        Task<IEnumerable<AnimalTagDTO>> GetAllAsync();

        Task<AnimalTagDTO?> GetByIdAsync(Guid animalTagId);

        Task<AnimalTagDTO> CreateAsync(AnimalTagDTO animalTagDTO);

        Task<AnimalTagDTO> UpdateAsync(AnimalTagDTO animalTagDTO);

        Task SoftDeleteAsync(Guid animalTagId);
    }
}