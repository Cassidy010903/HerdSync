using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Base
{
    public abstract class BaseEntity
    {
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}