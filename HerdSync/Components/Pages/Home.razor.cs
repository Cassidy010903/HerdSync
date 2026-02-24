using BLL.Services;
using HerdSync.Components.UniversalComponents.Calendar;
using HerdSync.Shared.DTO.Calendar;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages
{
    public partial class Home
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IPregnancyService PregnancyService { get; set; } = default!;
        [Inject] public ICalendarEventService CalendarEventService { get; set; } = default!;

        private int StockCount;
        private int PregnancyCount;
        private MudDropContainer<KanbanTaskItem> _dropContainer;
        private bool NewTaskOpen = false;
        private string NewTaskName;

        private List<HerdCalendar.CalendarEntry> calendarEntries = new();

        protected override async Task OnInitializedAsync()
        {
            var animals = await AnimalService.GetAllAsync();
            StockCount = animals.Count;

            var pregnancies = await PregnancyService.GetAllAsync();
            PregnancyCount = pregnancies.Count();

            var events = await CalendarEventService.GetAllAsync();
            calendarEntries = events.Select(e => new HerdCalendar.CalendarEntry
            {
                Id = e.CalendarEventId,
                Title = e.Title,
                Start = e.EventDate,
                End = e.EndDate ?? e.EventDate,
                Color = e.Color
            }).ToList();
        }

        public class KanbanTaskItem
        {
            public string Name { get; set; }

            public KanbanTaskItem(string name) => Name = name;

            public string Column { get; set; } = "Board";
        }

        private List<KanbanTaskItem> _tasks = new()
        {
            new KanbanTaskItem("Write unit test")
        };

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskName))
            {
                _tasks.Add(new KanbanTaskItem(NewTaskName));
                NewTaskName = string.Empty;
                NewTaskOpen = false;
                _dropContainer.Refresh();
            }
        }

        private async Task AddCalendarEntry(HerdCalendar.CalendarEntry entry)
        {
            var dto = new CalendarEventDTO
            {
                CalendarEventId = entry.Id,
                Title = entry.Title,
                EventDate = entry.Start,
                EndDate = entry.End,
                Color = entry.Color
            };
            var created = await CalendarEventService.CreateAsync(dto);
            calendarEntries.Add(new HerdCalendar.CalendarEntry
            {
                Id = created.CalendarEventId,
                Title = created.Title,
                Start = created.EventDate,
                End = created.EndDate ?? created.EventDate,
                Color = created.Color
            });
            StateHasChanged();
        }

        private async Task UpdateCalendarEntry(HerdCalendar.CalendarEntry entry)
        {
            var dto = new CalendarEventDTO
            {
                CalendarEventId = entry.Id,
                Title = entry.Title,
                EventDate = entry.Start,
                EndDate = entry.End,
                Color = entry.Color
            };
            await CalendarEventService.UpdateAsync(dto);
            var existing = calendarEntries.FirstOrDefault(e => e.Id == entry.Id);
            if (existing != null)
            {
                existing.Title = entry.Title;
                existing.Start = entry.Start;
                existing.End = entry.End;
                existing.Color = entry.Color;
            }
            StateHasChanged();
        }

        private async Task DeleteCalendarEntry(HerdCalendar.CalendarEntry entry)
        {
            await CalendarEventService.SoftDeleteAsync(entry.Id);
            calendarEntries.RemoveAll(e => e.Id == entry.Id);
            StateHasChanged();
        }
    }
}