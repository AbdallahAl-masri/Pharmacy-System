using Infrastructure.DTO;

namespace Service.Interfaces
{
    public interface ISessionService
    {
        Task<bool> IsSessionActiveAsync(SessionInfoDTO sessionDTO);
        Task RegisterSessionAsync(SessionInfoDTO sessionDTO);
        Task InvalidateSessionAsync(SessionInfoDTO sessionDTO);

    }
}
