using DAL.Models;

namespace DAL.Services
{
    public interface ITagRepository
    {
        public Task AddTag(stl_Species_Tag_Lookup stl);

        public Task UpdateTag(stl_Species_Tag_Lookup stl);

        public Task<List<stl_Species_Tag_Lookup>> GetAllTagsAsync();
    }
}