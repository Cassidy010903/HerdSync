using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Authentication
{
    [Table("UserAccount")]
    public class UserAccountModel : BaseEntity
    {
        [Key]
        public Guid UserId { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(100)]
        public string DisplayName { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        public bool IsActive { get; set; }
    }
}