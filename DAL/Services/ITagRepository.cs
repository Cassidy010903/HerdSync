using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface ITagRepository
    {
        public Task AddTag(stl_Species_Tag_Lookup stl);
        public Task UpdateTag(stl_Species_Tag_Lookup stl);
        public Task<List<stl_Species_Tag_Lookup>> GetAllTagsAsync();
    }
}
