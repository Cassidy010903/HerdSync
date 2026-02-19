using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramTemplateDTO : BaseEntityDTO
    {
        public string ProgramTemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}