using DAL.Models.Base;
using DAL.Models.Farm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Authentication
{
    [Table("FarmUser")]
    public class FarmUserModel : BaseEntity
    {
        [Key]
        public Guid FarmUserId { get; set; }

        public Guid FarmId { get; set; }
        public Guid UserId { get; set; }

        [Required, MaxLength(4)]
        public string RoleCode { get; set; }

        public bool IsActive { get; set; }

        // Navigation
        [ForeignKey("FarmId")]
        public FarmModel Farm { get; set; }

        [ForeignKey("UserId")]
        public UserAccountModel User { get; set; }

        [ForeignKey("RoleCode")]
        public UserRoleModel Role { get; set; }
    }
}