using DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Calendar
{
    [Table("CalendarEvent")]
    public class CalendarEventModel : BaseEntity
    {
        [Key]
        public Guid CalendarEventId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public DateTime EventDate { get; set; }
        public DateTime? EndDate { get; set; }

        [MaxLength(20)]
        public string Color { get; set; } = "primary";

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
