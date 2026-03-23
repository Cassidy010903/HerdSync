using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base
{
    public abstract class BaseEntity
    {
        [MaxLength(100)]
        public string? CreatedBy { get; set; } = new Guid().ToString();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}