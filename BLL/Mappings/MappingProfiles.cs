using AutoMapper;
using DAL.Models.Animal;
using DAL.Models.Authentication;
using DAL.Models.Calendar;
using DAL.Models.Farm;
using DAL.Models.Program.ProgramRun;
using DAL.Models.Program.ProgramTemplate;
using DAL.Models.Treatment;
using Kudde.Shared.DTO.Animal;
using Kudde.Shared.DTO.Authentication;
using Kudde.Shared.DTO.Calendar;
using Kudde.Shared.DTO.Farm;
using Kudde.Shared.DTO.Program;
using Kudde.Shared.DTO.Treatment;

namespace BLL.Mappings
{
    public class KuddeMappingProfile : Profile
    {
        public KuddeMappingProfile()
        {
            CreateMap<AnimalModel, AnimalDTO>().ReverseMap();
            CreateMap<AnimalEventTypeModel, AnimalEventTypeDTO>().ReverseMap();
            CreateMap<AnimalTagModel, AnimalTagDTO>().ReverseMap();
            CreateMap<AnimalTypeModel, AnimalTypeDTO>().ReverseMap();
            CreateMap<AnimalObservationModel, AnimalObservationDTO>().ReverseMap();
            CreateMap<CalendarEventModel, CalendarEventDTO>().ReverseMap();
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