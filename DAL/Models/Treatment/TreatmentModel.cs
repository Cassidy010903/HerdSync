using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Treatment
{
    [Table("Treatment")]
    public class TreatmentModel : BaseEntity
    {
        [Key, MaxLength(10)]
        public string TreatmentCode { get; set; }

        [Required, MaxLength(150)]
        public string TreatmentName { get; set; }

        [Required, MaxLength(6)]
        public string TreatmentCategoryCode { get; set; }

        // Navigation
        [ForeignKey("TreatmentCategoryCode")]
        public TreatmentCategoryModel TreatmentCategory { get; set; }
    }
}