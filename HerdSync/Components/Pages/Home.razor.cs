using MudBlazor;
using Radzen;
using Radzen.Blazor;

namespace HerdSync.Components.Pages
{
    public partial class Home
    {
        private RadzenScheduler<Appointment> scheduler;
        private Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

        private bool showHeader = true;

        private IList<Appointment> appointments = new List<Appointment>
        {
            new Appointment { Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-2), Text = "Birthday" },
            new Appointment
                { Start = DateTime.Today.AddDays(-11), End = DateTime.Today.AddDays(-10), Text = "Day off" },
            new Appointment
                { Start = DateTime.Today.AddDays(-10), End = DateTime.Today.AddDays(-8), Text = "Work from home" },
            new Appointment
                { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(12), Text = "Online meeting" },
            new Appointment
                { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(13), Text = "Skype call" },
            new Appointment
            {
                Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(14).AddMinutes(30),
                Text = "Dentist appointment"
            },
            new Appointment { Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(12), Text = "Vacation" },
        };

        private void OnDaySelect(SchedulerDaySelectEventArgs args)
        {
            Console.WriteLine($"DaySelect: Day={args.Day} AppointmentCount={args.Appointments.Count()}");
        }

        private void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            // Highlight today in month view
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] =
                    "background: var(--rz-scheduler-highlight-background-color, rgba(255,220,40,.2));";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Week" || args.View.Text == "Day") && args.Start.Hour > 8 && args.Start.Hour < 19)
            {
                args.Attributes["style"] =
                    "background: var(--rz-scheduler-highlight-background-color, rgba(255,220,40,.2));";
            }
        }

        private async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
            Console.WriteLine($"SlotSelect: Start={args.Start} End={args.End}");
        }

        private async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Appointment> args)
        {
            Console.WriteLine($"AppointmentSelect: Appointment={args.Data.Text}");

            await scheduler.Reload();
        }

        private void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<Appointment> args)
        {
            // Never call StateHasChanged in AppointmentRender - would lead to infinite loop

            if (args.Data.Text == "Birthday")
            {
                args.Attributes["style"] = "background: red";
            }
        }

        private async Task OnAppointmentMove(SchedulerAppointmentMoveEventArgs args)
        {
            var draggedAppointment = appointments.FirstOrDefault(x => x == args.Appointment.Data);

            await scheduler.Reload();
        }

        public class Appointment
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Text { get; set; }
        }

        private MudDropContainer<KanbanTaskItem> _dropContainer;
        private bool NewTaskOpen = false;
        private string NewTaskName;

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
                _dropContainer.Refresh(); // force redraw
            }
        }
    }
}