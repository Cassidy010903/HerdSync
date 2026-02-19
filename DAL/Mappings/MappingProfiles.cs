using AutoMapper;
using DAL.Models.Animal;
using DAL.Models.Authentication;
using DAL.Models.Farm;
using DAL.Models.Program;
using DAL.Models.Treatment;
using HerdSync.Shared.DTO.Animal;
using HerdSync.Shared.DTO.Authentication;
using HerdSync.Shared.DTO.Farm;
using HerdSync.Shared.DTO.Program;
using HerdSync.Shared.DTO.Treatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Mappings
{
    public class HerdSyncMappingProfile : Profile
    {
        public HerdSyncMappingProfile()
        {
            CreateMap<AnimalModel, AnimalDTO>().ReverseMap();
            CreateMap<AnimalEventTypeModel, AnimalEventTypeDTO>().ReverseMap();
            CreateMap<AnimalTagModel, AnimalTagDTO>().ReverseMap();
            CreateMap<AnimalTypeModel, AnimalTypeDTO>().ReverseMap();
            CreateMap<ConditionModel, ConditionDTO>().ReverseMap();
            CreateMap<FarmModel, FarmDTO>().ReverseMap();
            CreateMap<FarmActivityModel, FarmActivityDTO>().ReverseMap();
            CreateMap<FarmActivityTypeModel, FarmActivityTypeDTO>().ReverseMap();
            CreateMap<FarmUserModel, FarmUserDTO>().ReverseMap();
            CreateMap<PregnancyModel, PregnancyDTO>().ReverseMap();
            CreateMap<ProgramRunModel, ProgramRunDTO>().ReverseMap();
            CreateMap<ProgramRunAnimalModel, ProgramRunAnimalDTO>().ReverseMap();
            CreateMap<ProgramRunObservationModel, ProgramRunObservationDTO>().ReverseMap();
            CreateMap<ProgramRunTreatmentModel, ProgramRunTreatmentDTO>().ReverseMap();
            CreateMap<ProgramTemplateModel, ProgramTemplateDTO>().ReverseMap();
            CreateMap<ProgramTemplateRuleModel, ProgramTemplateRuleDTO>().ReverseMap();
            CreateMap<ProgramTemplateRuleTreatmentModel, ProgramTemplateRuleTreatmentDTO>().ReverseMap();
            CreateMap<TreatmentModel, TreatmentDTO>().ReverseMap();
            CreateMap<TreatmentCategoryModel, TreatmentCategoryDTO>().ReverseMap();
            CreateMap<TreatmentProductModel, TreatmentProductDTO>().ReverseMap();
            CreateMap<UserAccountModel, UserAccountDTO>()
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserRoleModel, UserRoleDTO>().ReverseMap();
        }
    }
}
