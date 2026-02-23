using HerdSync.Shared.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerdSync.Shared.DTO.Calendar
{
    public class CalendarEventDTO : BaseEntityDTO
    {
        public Guid CalendarEventId { get; set; }
        public string Title { get; set; } = "";
        public DateTime EventDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Color { get; set; } = "primary";
        public string? Description { get; set; }
    }
}
