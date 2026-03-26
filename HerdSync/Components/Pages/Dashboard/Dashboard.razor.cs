using BLL.Services;
using HerdSync.Components.Shared.Components;
using HerdSync.Components.Shared.Theme;
using HerdSync.Shared.DTO.Animal;
using HerdSync.Shared.DTO.Calendar;
using Microsoft.AspNetCore.Components;
using static BLL.Services.ActivityFeedService;

namespace HerdSync.Components.Pages.Dashboard
{
    public partial class Dashboard : IDisposable
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IPregnancyService PregnancyService { get; set; } = default!;
        [Inject] public ICalendarEventService CalendarEventService { get; set; } = default!;
        [Inject] public IAnimalObservationService AnimalObservationService { get; set; } = default!;
        [Inject] public IUserAccountService UserAccountService { get; set; } = default!;
        [Inject] public IProgramRunService ProgramRunService { get; set; } = default!;
        [Inject] public IProgramTemplateService ProgramTemplateService { get; set; } = default!;
        [Inject] public ActivityFeedService ActivityFeed { get; set; } = default!;

        private int StockCount;
        private int PregnancyCount;
        private List<AnimalDTO> _allAnimals = new();
        private List<ObservationDisplayItem> _observations = new();
        private List<HerdCalendar.CalendarEntry> calendarEntries = new();
        private Timer? _timer;

        private int CalfCount => _allAnimals.Count(a => GetAgeGroup(a.BirthYear) == "Calf");
        private int YearlingCount => _allAnimals.Count(a => GetAgeGroup(a.BirthYear) == "Yearling");
        private int AdultCount => _allAnimals.Count(a => GetAgeGroup(a.BirthYear) == "Adult");
        private int MaleCount => _allAnimals.Count(a => a.Gender == "M");
        private int FemaleCount => _allAnimals.Count(a => a.Gender == "F");

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

            var observations = await AnimalObservationService.GetAllAsync();
            _observations = observations
                .OrderByDescending(o => o.ObservationDate)
                .Take(10)
                .Select(o => new ObservationDisplayItem
                {
                    AnimalTag = _allAnimals.FirstOrDefault(a => a.AnimalId == o.AnimalId)?.DisplayIdentifier ?? o.AnimalId.ToString(),
                    ObservationDate = o.ObservationDate,
                    ConditionCode = o.ConditionCode,
                    Flag = o.Flag,
                    NumericValue = o.NumericValue,
                    TextValue = o.TextValue,
                    Notes = o.Notes,
                })
                .ToList();

            await BuildActivityFeed();

            ActivityFeed.OnChanged += OnActivityChanged;
            _timer = new Timer(_ => InvokeAsync(StateHasChanged), null, 60_000, 60_000);
        }

        private async Task BuildActivityFeed()
        {
            var entries = new List<ActivityEntry>();

            // New animals — earthy green
            foreach (var animal in _allAnimals.Where(a => a.CreatedDate != default).OrderByDescending(a => a.CreatedDate).Take(5))
            {
                entries.Add(new ActivityEntry
                {
                    Text = $"New animal added — #{animal.DisplayIdentifier}",
                    Timestamp = animal.CreatedDate,
                    Color = "#4a7c59",
                    Type = ActivityFeedService.ActivityType.NewAnimal
                });
            }

            // New users — muted slate
            var users = await UserAccountService.GetAllAsync();
            foreach (var user in users.Where(u => u.CreatedDate != default).OrderByDescending(u => u.CreatedDate).Take(5))
            {
                entries.Add(new ActivityEntry
                {
                    Text = $"New user joined — {user.DisplayName}",
                    Timestamp = user.CreatedDate,
                    Color = "#6b7f72",
                    Type = ActivityFeedService.ActivityType.NewUser
                });
            }

            // Program runs — warm brown
            var runs = await ProgramRunService.GetAllAsync();
            var templates = await ProgramTemplateService.GetAllAsync();
            foreach (var run in runs.OrderByDescending(r => r.RunDate).Take(5))
            {
                var name = templates.FirstOrDefault(t => t.ProgramTemplateCode == run.ProgramTemplateCode)?.TemplateName ?? run.ProgramTemplateCode;
                entries.Add(new ActivityEntry
                {
                    Text = $"Program run — {name}",
                    Timestamp = run.RunDate,
                    Color = "#8b6f47",
                    Type = ActivityFeedService.ActivityType.ProgramRun
                });
            }

            // Calendar events — muted golden earth
            foreach (var ev in calendarEntries.Where(e => e.Start != default).OrderByDescending(e => e.Start).Take(5))
            {
                entries.Add(new ActivityEntry
                {
                    Text = $"Event added — {ev.Title}",
                    Timestamp = ev.Start,
                    Color = "#c8a96e",
                    Type = ActivityFeedService.ActivityType.CalendarEvent
                });
            }

            ActivityFeed.Seed(entries.OrderByDescending(e => e.Timestamp).Take(9));
        }

        private void OnActivityChanged() => InvokeAsync(StateHasChanged);

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

            ActivityFeed.Add(new ActivityEntry
            {
                Text = $"Event added — {created.Title}",
                Timestamp = DateTime.Now,
                Color = "#c8a96e",
                Type = ActivityFeedService.ActivityType.CalendarEvent
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

        public static string GetRelativeTime(DateTime timestamp)
        {
            var diff = DateTime.Now - timestamp;
            var totalMinutes = diff.TotalMinutes;
            var totalHours = diff.TotalHours;
            var totalDays = diff.TotalDays;

            return totalMinutes switch
            {
                < 1 => "just now",
                < 2 => "1 min ago",
                <= 5 => $"{(int)totalMinutes} mins ago",
                _ when totalHours < 1 => "a few minutes ago",
                _ when totalHours < 2 => "an hour ago",
                _ when totalHours < 24 => "a few hours ago",
                _ when totalDays < 2 => "a day ago",
                _ => "a few days ago"
            };
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

        private string GetObservationColor(string? flag) => flag?.ToLower() switch
        {
            "high" => "#c0392b",
            "medium" => "#e67e22",
            "low" => HerdSyncColors.Primary,
            _ => HerdSyncColors.Primary
        };

        public void Dispose()
        {
            ActivityFeed.OnChanged -= OnActivityChanged;
            _timer?.Dispose();
        }

        public class ObservationDisplayItem
        {
            public string? AnimalTag { get; set; }
            public DateTime ObservationDate { get; set; }
            public string? ConditionCode { get; set; }
            public string? Flag { get; set; }
            public decimal? NumericValue { get; set; }
            public string? TextValue { get; set; }
            public string? Notes { get; set; }
        }
    }
}