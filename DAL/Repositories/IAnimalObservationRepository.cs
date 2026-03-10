using DAL.Models.Animal;
namespace DAL.Repositories
{
    public interface IAnimalObservationRepository
    {
        Task<IEnumerable<AnimalObservationModel>> GetAllAsync();
        Task<AnimalObservationModel?> GetByIdAsync(Guid animalObservationId);
        Task<AnimalObservationModel> AddAsync(AnimalObservationModel animalObservation);
        Task<AnimalObservationModel> UpdateAsync(AnimalObservationModel animalObservation);
        Task SoftDeleteAsync(Guid animalObservationId);
    }
}