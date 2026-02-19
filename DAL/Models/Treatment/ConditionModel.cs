using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Treatment
{
    [Table("Condition")]
    public class ConditionModel : BaseEntity
    {
        [Key, MaxLength(10)]
        public string ConditionCode { get; set; }

        [Required, MaxLength(150)]
        public string ConditionName { get; set; }

        [MaxLength(300)]
        public string ConditionDescription { get; set; }

        public bool IsInfectious { get; set; }
    }
}