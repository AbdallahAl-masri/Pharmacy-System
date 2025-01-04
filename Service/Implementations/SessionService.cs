using Infrastructure.DTO;
using Service.Interfaces;
using System.Collections.Concurrent;

namespace Service.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly ConcurrentDictionary<string, string> _activeSessions = new();

        public Task<bool> IsSessionActiveAsync(string userId, string token)
        {
            return Task.FromResult(_activeSessions.TryGetValue(userId, out var activeToken) && activeToken == token);
        }

        public Task RegisterSessionAsync(string userId, string token)
        {
            _activeSessions[userId] = token;
            return Task.CompletedTask;
        }

        public Task InvalidateSessionAsync(string userId)
        {
            _activeSessions.TryRemove(userId, out _);
            return Task.CompletedTask;
        }
    }
}
