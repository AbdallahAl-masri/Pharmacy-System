using Common;
using Infrastructure.DTO;
using Service.Interfaces;
using System.Security.Claims;

namespace API.Middleware
{
    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService, ISessionService sessionService)
        {
            var token = context.Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    if (jwtService.ValidateToken(token, out var claimsPrincipal))
                    {
                        var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

                        SessionInfoDTO sessionDTO = new SessionInfoDTO()
                        {
                            UserId = userId,
                            Token = token,
                        };

                        if (!string.IsNullOrEmpty(userId) && await sessionService.IsSessionActiveAsync(sessionDTO))
                        {
                            context.User = claimsPrincipal;
                        }
                        else
                        {
                            context.Response.Cookies.Delete("AuthToken");
                        }
                    }
                }
                catch
                {
                    context.Response.Cookies.Delete("AuthToken");
                }
            }

            await _next(context);
        }

    }

}
