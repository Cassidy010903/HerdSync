using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Farm
{
    [Table("FarmActivityType")]
    public class FarmActivityTypeModel : BaseEntity
    {
        [Key, MaxLength(8)]
        public string FarmActivityTypeCode { get; set; }

        [Required, MaxLength(50)]
        public string ActivityTypeName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}