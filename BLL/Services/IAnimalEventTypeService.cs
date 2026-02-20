using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IAnimalEventTypeService
    {
        Task<IEnumerable<AnimalEventTypeDTO>> GetAllAsync();
        Task<AnimalEventTypeDTO?> GetByIdAsync(string code);
        Task<AnimalEventTypeDTO> CreateAsync(AnimalEventTypeDTO animalEventTypeDTO);
        Task<AnimalEventTypeDTO> UpdateAsync(AnimalEventTypeDTO animalEventTypeDTO);
        Task SoftDeleteAsync(string code);
    }
}