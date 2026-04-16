using Kudde.Shared.DTO.Base;
namespace Kudde.Shared.DTO.Program
{
    public class ProgramTemplateDTO : BaseEntityDTO
    {
        public string ProgramTemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string? Frequency { get; set; }
    }
}