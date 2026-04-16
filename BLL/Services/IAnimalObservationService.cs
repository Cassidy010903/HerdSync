using Kudde.Shared.DTO.Animal;
namespace BLL.Services
{
    public interface IAnimalObservationService
    {
        Task<IEnumerable<AnimalObservationDTO>> GetAllAsync();
        Task<AnimalObservationDTO?> GetByIdAsync(Guid animalObservationId);
        Task<AnimalObservationDTO> CreateAsync(AnimalObservationDTO animalObservationDTO);
        Task<AnimalObservationDTO> UpdateAsync(AnimalObservationDTO animalObservationDTO);
        Task SoftDeleteAsync(Guid animalObservationId);
    }
}