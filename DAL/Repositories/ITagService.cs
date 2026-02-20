using HerdSync.Shared.DTO;

namespace DAL.Repositories
{
    public interface ITagService
    {
        public Task AddTagAsync(stl_Species_Tag_Lookup_DTO tagDTO);

        public Task UpdateTagAsync(stl_Species_Tag_Lookup_DTO tagDTO);

        public Task<List<stl_Species_Tag_Lookup_DTO>> GetAllTagsAsync();
    }
}