using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Treatment
{
    [Table("TreatmentProduct")]
    public class TreatmentProductModel : BaseEntity
    {
        [Key]
        public Guid TreatmentProductId { get; set; }

        [Required, MaxLength(10)]
        public string TreatmentCode { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; }

        [MaxLength(150)]
        public string ManufacturerName { get; set; }

        [MaxLength(300)]
        public string DosageInstructions { get; set; }

        // Navigation
        [ForeignKey("TreatmentCode")]
        public TreatmentModel Treatment { get; set; }
    }
}