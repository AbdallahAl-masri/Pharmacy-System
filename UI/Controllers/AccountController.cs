using Infrastructure.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using System.Security.Claims;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginUser(LoginDTO loginDTO)
        {
            var item = checkUser(loginDTO).Result;

            if (item == -1)
            {
                ViewBag.ErrorMessage = "Wrong user name or password!";
                return View("Login");
            }
            else
            {
                var userClaim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, loginDTO.UserName),
                    new Claim("UserName", loginDTO.UserName),
                    new Claim("UserID", item.ToString())
                };
                var identity = new ClaimsIdentity(userClaim, "User Identity");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
        }

        private async Task<int> checkUser(LoginDTO loginDTO)
        {

            var response = await _userService.Login(loginDTO);

            var Data = await response.Content.ReadAsStringAsync();
            int id = JsonConvert.DeserializeObject<int>(Data);

            return id;
        }

        public async Task<IActionResult> LogOut()
        {
            var _user = HttpContext.User as ClaimsPrincipal;
            var _identity = _user.Identity as ClaimsIdentity;

            foreach (var claim in _user.Claims.ToList())
            {
                _identity.RemoveClaim(claim);
            }
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}