using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramTemplateRuleTreatmentDTO : BaseEntityDTO
    {
        public Guid ProgramTemplateRuleTreatmentId { get; set; }
        public Guid ProgramTemplateRuleId { get; set; }
        public string TreatmentCode { get; set; }
        public bool IsMandatory { get; set; }
    }
}