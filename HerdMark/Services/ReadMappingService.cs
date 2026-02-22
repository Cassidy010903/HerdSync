using HerdMark.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text.Json;

namespace HerdMark.Services
{
    public class ReadMappingService
    {
        private readonly ConcurrentQueue<HerdMarkRead> _recent = new();
        private readonly ConcurrentDictionary<string, DateTime> _lastSeen = new();
        private readonly ILogger<ReadMappingService> _logger;
        private readonly string _logFilePath;

        public event Action<HerdMarkRead>? OnTagRead;

        public TimeSpan LockoutWindow { get; set; } = TimeSpan.FromSeconds(2);
        public IReadOnlyCollection<HerdMarkRead> RecentReads => _recent.ToArray();

        public ReadMappingService(IWebHostEnvironment env, ILogger<ReadMappingService> logger)
        {
            _logger = logger;
            _logFilePath = Path.Combine(env.ContentRootPath, "read-events.log");

            // No CSV logic for now
            _logger.LogInformation("ReadMappingService initialized (no mappings).");
        }

        // Stub for future mapping use
        public MarkedAnimal? LookupAthlete(string tagId) => null;

        public async Task AddReadAsync(HerdMarkRead read)
        {
            if (string.IsNullOrWhiteSpace(read.TagId)) return;

            var now = DateTime.UtcNow;
            if (_lastSeen.TryGetValue(read.TagId, out var last) &&
                (now - last) < LockoutWindow)
            {
                return; // duplicate
            }

            _lastSeen[read.TagId] = now;
            _recent.Enqueue(read);
            while (_recent.Count > 200)
                _recent.TryDequeue(out _);

            var json = JsonSerializer.Serialize(read);
            await File.AppendAllTextAsync(_logFilePath, json + Environment.NewLine);

            OnTagRead?.Invoke(read);
        }
    }
}