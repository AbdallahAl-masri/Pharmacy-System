using Common;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Implementations;
using Service.Interfaces;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ISessionService _sessionService;

        public AccountController(IUserService userService, IJwtService jwtService, ISessionService sessionService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _sessionService = sessionService;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            var token = await _userService.Login(loginDTO);

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrorMessage = "Wrong user name or password!";
                return View("Login");
            }
            else
            {
                // Retrieve UserID from the token
                var claims = _jwtService.GetClaims(token);
                var userId = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    // Register the session
                    await _sessionService.RegisterSessionAsync(userId, token);

                    // Store the JWT in a cookie
                    Response.Cookies.Append("AuthToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(8)
                    });
                }

                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            var token = Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
            {
                var claims = _jwtService.GetClaims(token);
                var userId = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    await _sessionService.InvalidateSessionAsync(userId);
                }

                Response.Cookies.Delete("AuthToken");
            }

            return RedirectToAction("Login", "Account");
        }

    }
}