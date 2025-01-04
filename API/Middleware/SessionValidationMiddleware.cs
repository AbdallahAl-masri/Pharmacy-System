using Common;
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
            // Retrieve the token from the request cookies
            var token = context.Request.Cookies["AuthToken"];

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Validate the token
                    if (jwtService.ValidateToken(token, out var claimsPrincipal))
                    {
                        // Extract the UserID claim from the token
                        var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

                        if (!string.IsNullOrEmpty(userId))
                        {
                            // Check if the session for this user is still active
                            if (await sessionService.IsSessionActiveAsync(userId, token))
                            {
                                // Set the ClaimsPrincipal in the HttpContext
                                context.User = claimsPrincipal;
                            }
                            else
                            {
                                // Invalidate the token if the session is not active
                                context.Response.Cookies.Delete("AuthToken");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (optional)
                    Console.WriteLine($"JWT validation error: {ex.Message}");
                    // You can also handle invalid tokens (e.g., clear the cookie)
                    context.Response.Cookies.Delete("AuthToken");
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }

}
