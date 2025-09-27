
using DAL.Models;
using HerdSync.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.Enums.Data;

namespace DAL.Services
{
    public interface ISpeciesRepository
    {
        public List<GenderEnum> GetGenderOptions(AnimalTypeEnum species);
        public List<AgeGroupEnum> GetAgeGroupOptions(AnimalTypeEnum species);
        public Task AddSpecies(spd_Species_Detail spd);
        public Task UpdateSpecies(spd_Species_Detail updated);
        public Task<List<spd_Species_Detail>> GetAllSpeciesAsync();
    }
}
