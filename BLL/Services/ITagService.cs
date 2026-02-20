using HerdSync.Shared.DTO;

namespace BLL.Services
{
    public interface ITagService
    {
        public Task AddTagAsync(stl_Species_Tag_Lookup_DTO tagDTO);

        public Task UpdateTagAsync(stl_Species_Tag_Lookup_DTO tagDTO);

        public Task<List<stl_Species_Tag_Lookup_DTO>> GetAllTagsAsync();
    }
}