//using AutoMapper;
//using DAL.Models;
//using DAL.Services;
//using HerdSync.Shared.DTO;
//using Microsoft.Extensions.Logging;

//namespace BLL.Services.Implementation
//{
//    public class TagService(IMapper mapper, ITagRepository repository, ILogger<TagService> logger) : ITagService
//    {
//        public async Task AddTagAsync(stl_Species_Tag_Lookup_DTO tagDTO)
//        {
//            // Apply business rules (e.g., no duplicate names, validations, etc.)
//            var tag = mapper.Map<stl_Species_Tag_Lookup>(tagDTO);

//            if (tag == null)
//                throw new InvalidOperationException("Mapping from DTO to entity failed.");

//            await repository.AddTag(tag);

//            logger.LogInformation("Added new tag with number ", tagDTO.stl_Tag_Id);
//        }

//        public async Task<List<stl_Species_Tag_Lookup_DTO>> GetAllTagsAsync()
//        {
//            var entities = await repository.GetAllTagsAsync();
//            return mapper.Map<List<stl_Species_Tag_Lookup_DTO>>(entities);
//        }

//        public async Task UpdateTagAsync(stl_Species_Tag_Lookup_DTO tagDTO)
//        {
//            var entity = mapper.Map<stl_Species_Tag_Lookup>(tagDTO);
//            await repository.UpdateTag(entity);
//        }
//    }
//}