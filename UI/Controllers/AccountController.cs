using Common;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Implementations;
using Service.Interfaces;
using System.Net;

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
            loginDTO.Token = Request.Cookies["AuthToken"];
            var response = await _userService.Login(loginDTO);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ViewBag.ErrorMessage = "Wrong username or password!";
                return View("Login");
            }
            if (response.IsSuccessStatusCode)
            {
                // Retrieve UserID from the token
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                string token = result.token;
                var claims = _jwtService.GetClaims(token);
                var userId = claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

                SessionInfoDTO sessionDTO = new SessionInfoDTO()
                {
                    UserId = userId,
                    Token = token,
                };

                // Register the session
                await _sessionService.InvalidateSessionAsync(sessionDTO);
                await _sessionService.RegisterSessionAsync(sessionDTO);

                // Store the JWT in a cookie
                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(5)
                });

                return RedirectToAction("Index", "Home");
            }
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<dynamic>(responseBody);
                ViewBag.ErrorMessage = error.message.ToString();
                return View("Login");
            }

            ViewBag.ErrorMessage = "An error occurred while logging in.";
            return View("Login");
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
                    SessionInfoDTO sessionDTO = new SessionInfoDTO()
                    {
                        UserId = userId,
                    };
                    await _sessionService.InvalidateSessionAsync(sessionDTO);
                }

                Response.Cookies.Delete("AuthToken");
            }

            return RedirectToAction("Login", "Account");
        }


    }
}