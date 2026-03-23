using DAL.Models.Base;
using DAL.Models.Farm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Authentication
{
    [Table("FarmInvite")]
    public class FarmInviteModel : BaseEntity
    {
        [Key]
        public Guid InviteId { get; set; }

        public Guid FarmId { get; set; }

        [Required, MaxLength(10)]
        public string InviteCode { get; set; }

        [Required, MaxLength(4)]
        public string AssignedRoleCode { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; } = false;

        public Guid? UsedByUserId { get; set; }

        // Navigation
        [ForeignKey("FarmId")]
        public FarmModel Farm { get; set; }

        [ForeignKey("AssignedRoleCode")]
        public UserRoleModel Role { get; set; }
    }
}
