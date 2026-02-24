using DAL.Configuration.Database;
using DAL.Models.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class CalendarEventRepository(HerdsyncDBContext context, ILogger<CalendarEventRepository> logger) : ICalendarEventRepository
    {
        public async Task<IEnumerable<CalendarEventModel>> GetAllAsync()
            => await context.CalendarEvents.Where(c => !c.IsDeleted).ToListAsync();

        public async Task<CalendarEventModel?> GetByIdAsync(Guid calendarEventId)
            => await context.CalendarEvents.FirstOrDefaultAsync(c => c.CalendarEventId == calendarEventId && !c.IsDeleted);

        public async Task<CalendarEventModel> AddAsync(CalendarEventModel calendarEvent)
        {
            context.CalendarEvents.Add(calendarEvent);
            await context.SaveChangesAsync();
            logger.LogInformation("Added calendar event {CalendarEventId}", calendarEvent.CalendarEventId);
            return calendarEvent;
        }

        public async Task<CalendarEventModel> UpdateAsync(CalendarEventModel calendarEvent)
        {
            var existing = await context.CalendarEvents.FirstOrDefaultAsync(c => c.CalendarEventId == calendarEvent.CalendarEventId);
            if (existing == null) throw new KeyNotFoundException($"CalendarEvent {calendarEvent.CalendarEventId} not found.");

            existing.Title = calendarEvent.Title;
            existing.EventDate = calendarEvent.EventDate;
            existing.EndDate = calendarEvent.EndDate;
            existing.Color = calendarEvent.Color;
            existing.Description = calendarEvent.Description;

            await context.SaveChangesAsync();
            logger.LogInformation("Updated calendar event {CalendarEventId}", existing.CalendarEventId);
            return existing;
        }

        public async Task SoftDeleteAsync(Guid calendarEventId)
        {
            var entity = await context.CalendarEvents.FirstOrDefaultAsync(c => c.CalendarEventId == calendarEventId);
            if (entity == null) throw new KeyNotFoundException($"CalendarEvent {calendarEventId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted calendar event {CalendarEventId}", calendarEventId);
        }
    }
}