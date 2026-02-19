using System.Net.Http.Json;
using HerdMark.Models;

class Program
{
    static async Task Main()
    {
        var client = new HttpClient();
        var serverUrl = "http://localhost:5000/api/reads";
        bool enableSimulation = false; //change this to true to read real time data automatically


        var rand = new Random();

        var tags = new[]
        {
            "E200001234567890",
            "E200001234567891",
            "E200001234567892"
        };

        Console.WriteLine($"Simulator sending reads to {serverUrl}");
        if (enableSimulation)
        {
            while (true)
            {
                var tag = tags[rand.Next(tags.Length)];
                var read = new HerdMarkRead
                {
                    TagId = tag,
                    ReaderId = "sim-reader-1",
                    AntennaPort = 1,
                    TimestampUtc = DateTime.UtcNow,
                    Rssi = rand.Next(-70, -40),
                    Frequency = 915_000_000
                };

                try
                {
                    var res = await client.PostAsJsonAsync(serverUrl, new[] { read });
                    Console.WriteLine($"Sent {tag}, status {res.StatusCode}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                await Task.Delay(1000);
            }
        }
    }
}