using HerdSync.Shared.DTO;
using HerdSync.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAnimalService
    {
        public Task AddAnimalAsync(spd_Species_Detail_DTO speciesDTO);
        Task<List<spd_Species_Detail_DTO>> GetAllHerdAsync();
        public Task UpdateAnimalAsync(spd_Species_Detail_DTO dto);
    }
}
