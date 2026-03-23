using DAL.Models.Authentication;
using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Farm
{
    [Table("Farm")]
    public class FarmModel
    {
        [Key]
        public Guid FarmId { get; set; }

        [Required, MaxLength(150)]
        public string FarmName { get; set; }

        public Guid OwnerUserId { get; set; }

        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("OwnerUserId")]
        public UserAccountModel Owner { get; set; }
    }
}
