using AutoMapper;
using DAL.Configuration.Database;
using DAL.Models;
using DAL.Services;
using DAL.Services.Implementation;
using HerdSync.Shared;
using HerdSync.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class AnimalService(IMapper mapper, ISpeciesRepository repository, ILogger<AnimalService> logger) : IAnimalService
    {
        public async Task AddAnimalAsync(spd_Species_Detail_DTO speciesDTO)
        {
            // Apply business rules (e.g., no duplicate names, validations, etc.)
            var animal = mapper.Map<spd_Species_Detail>(speciesDTO);

            if (animal == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");

            await repository.AddSpecies(animal);

            logger.LogInformation("Added new animal with number {CowNumber}", speciesDTO.spd_Number);
        }

        public async Task<List<spd_Species_Detail_DTO>> GetAllHerdAsync()
        {
            var entities = await repository.GetAllSpeciesAsync();
            return mapper.Map<List<spd_Species_Detail_DTO>>(entities);

        }


        public async Task UpdateAnimalAsync(spd_Species_Detail_DTO dto)
        {
            var entity = mapper.Map<spd_Species_Detail>(dto);
            await repository.UpdateSpecies(entity);
        }

    }
}
