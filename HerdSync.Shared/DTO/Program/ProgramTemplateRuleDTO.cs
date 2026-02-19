using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramTemplateRuleDTO : BaseEntityDTO
    {
        public Guid ProgramTemplateRuleId { get; set; }
        public string ProgramTemplateCode { get; set; }
        public string AnimalTypeCode { get; set; }
        public string Gender { get; set; }
        public int? MinBirthYear { get; set; }
        public int? MaxBirthYear { get; set; }
    }
}