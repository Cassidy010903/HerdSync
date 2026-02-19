using HerdSync.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.DTO.Animal;

namespace BLL.Services
{
    public interface IAnimalService
    {
        public Task AddAnimalAsync(AnimalDTO animalDTO);
        Task<List<AnimalDTO>> GetAllHerdAsync();
        public Task UpdateAnimalAsync(AnimalDTO animalDTO);
    }
}
