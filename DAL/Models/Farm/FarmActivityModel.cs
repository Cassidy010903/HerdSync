using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Farm
{
    [Table("FarmActivity")]
    public class FarmActivityModel : BaseEntity
    {
        [Key]
        public Guid FarmActivityId { get; set; }

        public Guid AnimalId { get; set; }

        [Required, MaxLength(8)]
        public string FarmActivityTypeCode { get; set; }

        public DateTime ActivityDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public int? Quantity { get; set; }

        [MaxLength(50)]
        public string ReferenceNumber { get; set; }

        // Navigation
        [ForeignKey("AnimalId")]
        public AnimalModel Animal { get; set; }

        [ForeignKey("FarmActivityTypeCode")]
        public FarmActivityTypeModel FarmActivityType { get; set; }
    }
}