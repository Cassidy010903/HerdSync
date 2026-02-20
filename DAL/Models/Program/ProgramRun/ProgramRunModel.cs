using DAL.Models.Base;
using DAL.Models.Program.ProgramTemplate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program.ProgramRun
{
    [Table("ProgramRun")]
    public class ProgramRunModel : BaseEntity
    {
        [Key]
        public Guid ProgramRunId { get; set; }

        [Required, MaxLength(10)]
        public string ProgramTemplateCode { get; set; }

        public DateTime RunDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Navigation
        [ForeignKey("ProgramTemplateCode")]
        public ProgramTemplateModel ProgramTemplate { get; set; }
    }
}