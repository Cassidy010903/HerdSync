using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Program.ProgramTemplate
{
    [Table("ProgramTemplate")]
    public class ProgramTemplateModel : BaseEntity
    {
        [Key, MaxLength(10)]
        public string ProgramTemplateCode { get; set; }

        [Required, MaxLength(150)]
        public string TemplateName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}