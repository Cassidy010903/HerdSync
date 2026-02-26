using BLL.Services;
using HerdSync.Components.Shared.Components;
using HerdSync.Shared.DTO.Animal;
using HerdSync.Shared.DTO.Calendar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace HerdSync.Components.Pages.Dashboard
{
    public partial class Dashboard
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IPregnancyService PregnancyService { get; set; } = default!;
        [Inject] public ICalendarEventService CalendarEventService { get; set; } = default!;

        private int StockCount;
        private int PregnancyCount;
        private List<AnimalDTO> _allAnimals = new();

        private int CalfCount => _allAnimals.Count(a => GetAgeGroup(a.BirthYear) == "Calf");
        private int YearlingCount => _allAnimals.Count(a => GetAgeGroup(a.BirthYear) == "Yearling");
        private int AdultCount => _allAnimals.Count(a => GetAgeGroup(a.BirthYear) == "Adult");
        private int MaleCount => _allAnimals.Count(a => a.Gender == "M");
        private int FemaleCount => _allAnimals.Count(a => a.Gender == "F");

        private List<HerdCalendar.CalendarEntry> calendarEntries = new();

        private IEnumerable<HerdCalendar.CalendarEntry> UpcomingEvents =>
            calendarEntries
                .Where(e => e.Start.Date >= DateTime.Today)
                .OrderBy(e => e.Start)
                .Take(5);

        private string NextEventDate =>
            calendarEntries
                .Where(e => e.Start > DateTime.Now)
                .OrderBy(e => e.Start)
                .FirstOrDefault()?.Start.ToString("dd MMM yyyy") ?? "None";

        private bool _addingNote = false;
        private string _newNoteText = string.Empty;
        private List<string> _notes = new()
        {
            "Check water troughs in the north paddock.",
            "Bull #B-0055 showing signs of lameness.",
        };

        private List<ActivityItem> RecentActivity = new()
        {
            new() { Text = "Annual Dip — 42 animals treated",   Time = "2 days ago",  Color = "#4a7c59" },
            new() { Text = "Tag assigned to #A-0201",            Time = "3 days ago",  Color = "#3a7fa8" },
            new() { Text = "Cow #C-0088 added to herd",         Time = "5 days ago",  Color = "#6a9e78" },
            new() { Text = "Quarterly Vitamin Run — 38 treated", Time = "2 weeks ago", Color = "#4a7c59" },
            new() { Text = "Tag #RFID-00412 reassigned",         Time = "3 weeks ago", Color = "#d4a843" },
        };

        protected override async Task OnInitializedAsync()
        {
            _allAnimals = await AnimalService.GetAllAsync();
            StockCount = _allAnimals.Count;

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

        private void AddNote()
        {
            if (!string.IsNullOrWhiteSpace(_newNoteText))
            {
                _notes.Add(_newNoteText.Trim());
                _newNoteText = string.Empty;
                _addingNote = false;
            }
        }

        private void HandleNoteKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter") AddNote();
            if (e.Key == "Escape") _addingNote = false;
        }

        private string GetAgeGroup(int? birthYear)
        {
            if (birthYear == null) return "Unknown";
            return (DateTime.Today.Year - birthYear.Value) switch
            {
                <= 1 => "Calf",
                <= 2 => "Yearling",
                _ => "Adult"
            };
        }

        private int GetPercent(int count)
        {
            if (StockCount == 0) return 0;
            return (int)Math.Round((double)count / StockCount * 100);
        }

        private string GetRelativeDate(DateTime date)
        {
            var diff = (date.Date - DateTime.Today).Days;
            return diff switch
            {
                0 => "Today",
                1 => "Tomorrow",
                _ => $"In {diff} days"
            };
        }

        public class ActivityItem
        {
            public string Text { get; set; } = "";
            public string Time { get; set; } = "";
            public string Color { get; set; } = "#4a7c59";
        }
    }
}