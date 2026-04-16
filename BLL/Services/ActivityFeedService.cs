using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ActivityFeedService
    {
        public event Action? OnChanged;

        private readonly List<ActivityEntry> _entries = new();

        public IReadOnlyList<ActivityEntry> Entries => _entries.AsReadOnly();

        public void Add(ActivityEntry entry)
        {
            _entries.Insert(0, entry);
            if (_entries.Count > 50) _entries.RemoveAt(_entries.Count - 1);
            OnChanged?.Invoke();
        }

        public void Seed(IEnumerable<ActivityEntry> entries)
        {
            _entries.Clear();
            _entries.AddRange(entries.OrderByDescending(e => e.Timestamp));
            OnChanged?.Invoke();
        }

        public class ActivityEntry
        {
            public string Text { get; set; } = "";
            public DateTime Timestamp { get; set; }
            public string Color { get; set; } = "#4a7c59";
            public ActivityType Type { get; set; }
        }

        public enum ActivityType
        {
            NewUser,
            NewAnimal,
            ProgramRun,
            CalendarEvent
        }
    }
}