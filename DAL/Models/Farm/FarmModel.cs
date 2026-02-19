using DAL.Models.Authentication;
using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Farm
{
    [Table("Farm")]
    public class FarmModel : BaseEntity
    {
        [Key]
        public Guid FarmId { get; set; }

        [Required, MaxLength(150)]
        public string FarmName { get; set; }

        public Guid OwnerUserId { get; set; }

        // Navigation
        [ForeignKey("OwnerUserId")]
        public UserAccountModel Owner { get; set; }
    }
}