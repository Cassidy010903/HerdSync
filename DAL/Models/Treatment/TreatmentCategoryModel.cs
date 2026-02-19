using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Treatment
{
    [Table("TreatmentCategory")]
    public class TreatmentCategoryModel : BaseEntity
    {
        [Key, MaxLength(6)]
        public string TreatmentCategoryCode { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; }
    }
}