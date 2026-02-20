using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Base
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public string CreatedUser { get; set; } = "System"; //Update this to take the name of the logged-in user

        public DateTime? Updated { get; set; }

        [MaxLength(100)]
        public string? UpdatedUser { get; set; }
    }
}