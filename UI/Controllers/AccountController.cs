using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
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
                var principal = new ClaimsPrincipal(new[] { identity });
                HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
        }

        private async Task<int> checkUser(LoginDTO loginDTO)
        {
            Console.WriteLine(ConfigSettings.BaseApiUrl);
            HttpClient client = new HttpClient();
            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "User/Login", new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));

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