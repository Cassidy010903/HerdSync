using HerdSync.Shared.DTO.Authentication;

namespace HerdSync.Shared.DTO.Base
{
    public abstract class BaseEntityDTO
    {
        public string CreatedBy { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}