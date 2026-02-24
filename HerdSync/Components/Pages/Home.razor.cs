using BLL.Services;
using HerdSync.Components.UniversalComponents.Calendar;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HerdSync.Components.Pages
{
    public partial class Home
    {
        [Inject] public IAnimalService AnimalService { get; set; } = default!;
        [Inject] public IPregnancyService PregnancyService { get; set; } = default!;

        private int StockCount;
        private int PregnancyCount;
        private MudDropContainer<KanbanTaskItem> _dropContainer;
        private bool NewTaskOpen = false;
        private string NewTaskName;

        private List<HerdCalendar.CalendarEntry> calendarEntries = new()
        {
            new() { Date = DateTime.Today.AddDays(3), Title = "Vaccination", Color = "success" },
            new() { Date = DateTime.Today.AddDays(7), Title = "Vet Visit", Color = "warning" },
            new() { Date = DateTime.Today, Title = "Dip Day", Color = "danger" },
        };

        protected override async Task OnInitializedAsync()
        {
            var animals = await AnimalService.GetAllAsync();
            StockCount = animals.Count;

            var pregnancies = await PregnancyService.GetAllAsync();
            PregnancyCount = pregnancies.Count();
        }
        public class Appointment
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Text { get; set; }
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
    }
}