using DAL.Models.Animal;
using HerdSync.Shared.Enums.Data;

namespace DAL.Services
{
    public interface ISpeciesRepository
    {
        public List<GenderEnum> GetGenderOptions(AnimalTypeEnum species);

        public List<AgeGroupEnum> GetAgeGroupOptions(AnimalTypeEnum species);

        public Task AddSpecies(AnimalModel spd);

        public Task UpdateSpecies(AnimalModel updated);

        public Task<List<AnimalModel>> GetAllSpeciesAsync();
    }
    public interface IAnimalRepository
    {
        Task<IEnumerable<AnimalModel>> GetAllAsync();
        Task<AnimalModel?> GetByIdAsync(Guid animalId);
        Task<AnimalModel> AddAsync(AnimalModel animal);
        Task<AnimalModel> UpdateAsync(AnimalModel animal);
        Task SoftDeleteAsync(Guid animalId);
    }
}