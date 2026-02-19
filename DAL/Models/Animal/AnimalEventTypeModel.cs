using DAL.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Animal
{
    [Table("AnimalEventType")]
    public class AnimalEventTypeModel : BaseEntity
    {
        [Key, MaxLength(5)]
        public string EventTypeCode { get; set; }

        [Required, MaxLength(100)]
        public string EventTypeName { get; set; }
    }
}