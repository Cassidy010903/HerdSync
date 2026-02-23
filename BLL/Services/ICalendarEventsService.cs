using HerdSync.Shared.DTO.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICalendarEventService
    {
        Task<IEnumerable<CalendarEventDTO>> GetAllAsync();
        Task<CalendarEventDTO?> GetByIdAsync(Guid calendarEventId);
        Task<CalendarEventDTO> CreateAsync(CalendarEventDTO calendarEventDTO);
        Task<CalendarEventDTO> UpdateAsync(CalendarEventDTO calendarEventDTO);
        Task SoftDeleteAsync(Guid calendarEventId);
    }
}
