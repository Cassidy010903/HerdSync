using DAL.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICalendarEventRepository
    {
        Task<IEnumerable<CalendarEventModel>> GetAllAsync();
        Task<CalendarEventModel?> GetByIdAsync(Guid calendarEventId);
        Task<CalendarEventModel> AddAsync(CalendarEventModel calendarEvent);
        Task<CalendarEventModel> UpdateAsync(CalendarEventModel calendarEvent);
        Task SoftDeleteAsync(Guid calendarEventId);
    }
}
