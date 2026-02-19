using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using HerdSync.Shared.Enums.Data;

namespace DAL.Services
{
    public interface IProgramRepository
    {
        Task<prg_Program> CreateAsync(
            string name,
            IEnumerable<(AgeGroupEnum? age, GenderEnum? gender, AnimalTypeEnum? species, IEnumerable<Guid> treatmentIds)> rules);
        public Task<List<prg_Program>> ListAsync();
        public Task<prg_Program?> GetAsync(Guid programId);
    }
}
