using HerdSync.Shared.DTO.Base;

namespace HerdSync.Shared.DTO.Program
{
    public class ProgramRunDTO : BaseEntityDTO
    {
        public Guid ProgramRunId { get; set; }
        public string ProgramTemplateCode { get; set; }
        public DateTime RunDate { get; set; }
        public string Notes { get; set; }
    }
}