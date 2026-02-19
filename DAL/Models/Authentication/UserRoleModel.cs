using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Authentication
{
    [Table("UserRole")]
    public class UserRoleModel : BaseEntity
    {
        [Key, MaxLength(4)]
        public string RoleCode { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }
    }
}