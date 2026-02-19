using AutoMapper;
using DAL.Configuration.Database;
using DAL.Models;
using DAL.Models.Animal;
using DAL.Services;
using DAL.Services.Implementation;
using HerdSync.Shared;
using HerdSync.Shared.DTO;
using HerdSync.Shared.DTO.Animal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalService(IMapper mapper, ISpeciesRepository repository, ILogger<AnimalService> logger) : IAnimalService
    {
        public async Task AddAnimalAsync(AnimalDTO animalDTO)
        {
            var animal = mapper.Map<AnimalModel>(animalDTO);

            if (animal == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");

            await repository.AddSpecies(animal);

            logger.LogInformation("Added new animal with number {CowNumber}", animal.DisplayIdentifier);
        }

        public async Task<List<AnimalDTO>> GetAllHerdAsync()
        {
            var entities = await repository.GetAllSpeciesAsync();
            return mapper.Map<List<AnimalDTO>>(entities);

        }


        public async Task UpdateAnimalAsync(AnimalDTO animalDTO)
        {
            var entity = mapper.Map<AnimalModel>(animalDTO);
            await repository.UpdateSpecies(entity);
        }

    }
}
