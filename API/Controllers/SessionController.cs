using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpPost("active")]
        public IActionResult IsSessionActive([FromBody] SessionInfoDTO sessionDTO)
        {
            var session = _sessionRepository.Find(s => s.UserId == sessionDTO.UserId).FirstOrDefault();

            return Ok(session != null && session.ExpiryDate > DateTime.UtcNow);
        }

        [HttpPost]
        public IActionResult RegisterSessionAsync(SessionInfoDTO sessionDTO)
        {
            // Invalidate previous sessions for the user
            var existingSessions = _sessionRepository.Find(s => s.UserId == sessionDTO.UserId).FirstOrDefault();

            if (existingSessions != null)
                _sessionRepository.Delete(existingSessions);

            // Register the new session
            var session = new ActiveSession
            {
                UserId = sessionDTO.UserId,
                Token = sessionDTO.Token,
                ExpiryDate = DateTime.UtcNow.AddMinutes(5),
            };

            _sessionRepository.Add(session);

            return Ok(session.Token);
        }

        [HttpPost("invalidate")]
        public IActionResult InvalidateSession([FromBody] SessionInfoDTO sessionDTO)
        {
            var session = _sessionRepository.Find(s => s.UserId == sessionDTO.UserId).FirstOrDefault();

            if (session != null)
            {
                _sessionRepository.Delete(session);
            }

            return Ok();
        }
    }

}
