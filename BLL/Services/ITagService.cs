using HerdSync.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ITagService
    {
        public Task AddTagAsync(stl_Species_Tag_Lookup_DTO tagDTO);
        public Task UpdateTagAsync(stl_Species_Tag_Lookup_DTO tagDTO);
        public Task<List<stl_Species_Tag_Lookup_DTO>> GetAllTagsAsync();
    }
}
