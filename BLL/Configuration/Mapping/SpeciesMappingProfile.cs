using AutoMapper;
using DAL.Models;
using HerdSync.Shared.Enums.Data;
using HerdSync.Shared.DTO;
using HerdSync.Shared.Enums.Data.Extensions;

namespace BLL.Configuration.Mapping
{
    public class SpeciesMappingProfile : Profile
    {
        public SpeciesMappingProfile()
        {
            // Entity → DTO (convert display strings back to enums)
            CreateMap<spd_Species_Detail, spd_Species_Detail_DTO>()
                .ForMember(dest => dest.spd_Species,
                    opt => opt.MapFrom(src => Enum.Parse<AnimalTypeEnum>(src.spd_Species)))
                .ForMember(dest => dest.spd_Gender, opt => opt.MapFrom(src =>
                    Enum.GetValues<GenderEnum>().FirstOrDefault(e =>
                        e.ToDisplayGender(Enum.Parse<AnimalTypeEnum>(src.spd_Species)) == src.spd_Gender)))
                .ForMember(dest => dest.spd_AgeGroup, opt => opt.MapFrom(src =>
                    Enum.GetValues<AgeGroupEnum>().FirstOrDefault(e =>
                        e.ToDisplayName(Enum.Parse<AnimalTypeEnum>(src.spd_Species)) == src.spd_AgeGroup)))
                .ForMember(dest => dest.spd_Tag_Colour, opt => opt.MapFrom(src =>
                    Enum.Parse<ColourEnum>(src.spd_Tag_Colour)))
                .ForMember(dest => dest.spd_Id, opt => opt.MapFrom(src => src.Id));

        // DTO → Entity
        CreateMap<spd_Species_Detail_DTO, spd_Species_Detail>()
            .ForMember(dest => dest.spd_Gender,
                opt => opt.MapFrom(src => src.spd_Gender.ToDisplayGender(src.spd_Species)))
            .ForMember(dest => dest.spd_AgeGroup,
                opt => opt.MapFrom(src => src.spd_AgeGroup.ToDisplayName(src.spd_Species)))
            .ForMember(dest => dest.spd_Species, opt => opt.MapFrom(src => src.spd_Species.ToString()))
            .ForMember(dest => dest.spd_Tag_Colour, opt => opt.MapFrom(src => src.spd_Tag_Colour.ToString()))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.spd_Id));

            CreateMap<prg_Pregnancies_Detail, prg_Pregnancies_Detail_DTO>();
        }
    }
}