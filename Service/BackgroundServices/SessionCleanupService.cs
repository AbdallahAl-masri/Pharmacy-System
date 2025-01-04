using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BackgroundServices
{
    public class SessionCleanupService : BackgroundService
    {
        // Thread-safe dictionary to store active sessions and their expiration times
        private static readonly ConcurrentDictionary<string, string> ActiveSessions = new ConcurrentDictionary<string, string>();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Clean expired sessions every 30 minutes
                    var expiredKeys = ActiveSessions
                        .Where(pair => DateTime.UtcNow > DateTime.Parse(pair.Value))
                        .Select(pair => pair.Key)
                        .ToList();

                    foreach (var key in expiredKeys)
                    {
                        // Safely remove expired sessions
                        if (ActiveSessions.TryRemove(key, out _))
                        {
                            Console.WriteLine($"Session with key {key} removed.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exceptions
                    Console.WriteLine($"Error during session cleanup: {ex.Message}");
                }

                // Wait 30 minutes before the next cleanup
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }

        // Method to add or update a session
        public void AddOrUpdateSession(string sessionId, DateTime expirationTime)
        {
            ActiveSessions[sessionId] = expirationTime.ToString("o"); // ISO 8601 format
        }

        // Method to remove a session manually
        public void RemoveSession(string sessionId)
        {
            ActiveSessions.TryRemove(sessionId, out _);
        }
    }
}
