
using DAL.Models;
using HerdSync.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.Enums.Data;
using DAL.Models.Animal;

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
}
