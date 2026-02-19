using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program
{
    [Table("ProgramTemplateRule")]
    public class ProgramTemplateRuleModel : BaseEntity
    {
        [Key]
        public Guid ProgramTemplateRuleId { get; set; }

        [Required, MaxLength(10)]
        public string ProgramTemplateCode { get; set; }

        [Required, MaxLength(6)]
        public string AnimalTypeCode { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        public int? MinBirthYear { get; set; }
        public int? MaxBirthYear { get; set; }

        // Navigation
        [ForeignKey("ProgramTemplateCode")]
        public ProgramTemplateModel ProgramTemplate { get; set; }

        [ForeignKey("AnimalTypeCode")]
        public AnimalTypeModel AnimalType { get; set; }
    }
}