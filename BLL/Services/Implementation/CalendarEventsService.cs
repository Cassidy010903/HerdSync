using AutoMapper;
using DAL.Models.Calendar;
using DAL.Repositories;
using HerdSync.Shared.DTO.Calendar;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class CalendarEventService(IMapper mapper, ICalendarEventRepository repository, ILogger<CalendarEventService> logger) : ICalendarEventService
    {
        public async Task<IEnumerable<CalendarEventDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<CalendarEventDTO>>(entities);
        }

        public async Task<CalendarEventDTO?> GetByIdAsync(Guid calendarEventId)
        {
            var entity = await repository.GetByIdAsync(calendarEventId);
            return entity == null ? null : mapper.Map<CalendarEventDTO>(entity);
        }

        public async Task<CalendarEventDTO> CreateAsync(CalendarEventDTO calendarEventDTO)
        {
            var entity = mapper.Map<CalendarEventModel>(calendarEventDTO);
            if (entity == null) throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            return mapper.Map<CalendarEventDTO>(created);
        }

        public async Task<CalendarEventDTO> UpdateAsync(CalendarEventDTO calendarEventDTO)
        {
            calendarEventDTO.ModifiedDate = DateTime.UtcNow;
            //calendarEventDTO.ModifiedBy = GetCurrentUser(); //Implement later
            var entity = mapper.Map<CalendarEventModel>(calendarEventDTO);
            if (entity == null) throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            return mapper.Map<CalendarEventDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid calendarEventId)
        {
            await repository.SoftDeleteAsync(calendarEventId);
            logger.LogInformation("Soft deleted calendar event {CalendarEventId}", calendarEventId);
        }
    }
}
