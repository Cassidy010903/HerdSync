using DAL.Models.Base;
using DAL.Models.Treatment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program.ProgramTemplate
{
    [Table("ProgramTemplateRuleTreatment")]
    public class ProgramTemplateRuleTreatmentModel : BaseEntity
    {
        [Key]
        public Guid ProgramTemplateRuleTreatmentId { get; set; }

        public Guid ProgramTemplateRuleId { get; set; }

        [Required, MaxLength(10)]
        public string TreatmentCode { get; set; }

        public bool IsMandatory { get; set; }

        // Navigation
        [ForeignKey("ProgramTemplateRuleId")]
        public ProgramTemplateRuleModel ProgramTemplateRule { get; set; }

        [ForeignKey("TreatmentCode")]
        public TreatmentModel Treatment { get; set; }
    }
}