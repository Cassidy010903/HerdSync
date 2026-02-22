using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IAnimalTypeService
    {
        Task<IEnumerable<AnimalTypeDTO>> GetAllAsync();

        Task<AnimalTypeDTO?> GetByIdAsync(string code);

        Task<AnimalTypeDTO> CreateAsync(AnimalTypeDTO animalTypeDTO);

        Task<AnimalTypeDTO> UpdateAsync(AnimalTypeDTO animalTypeDTO);

        Task SoftDeleteAsync(string code);
    }
}