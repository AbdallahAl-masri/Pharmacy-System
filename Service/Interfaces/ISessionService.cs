namespace Service.Interfaces
{
    public interface ISessionService
    {
        Task<bool> IsSessionActiveAsync(string userId, string token);
        Task RegisterSessionAsync(string userId, string token);
        Task InvalidateSessionAsync(string userId);

    }
}
