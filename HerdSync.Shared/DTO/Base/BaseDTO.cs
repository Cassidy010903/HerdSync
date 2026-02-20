using System.ComponentModel.DataAnnotations;

namespace HerdSync.Shared.DTO.Base
{
    public class BaseDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public string Created_User { get; set; } = Environment.UserName;

        public DateTime? Updated { get; set; }
        public string? Updated_User { get; set; }
    }
}