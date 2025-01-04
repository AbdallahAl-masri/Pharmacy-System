using Common;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Infrastructure.Base
{
    public class BaseController : Controller
    {
        private readonly IJwtService _jwtService;

        public BaseController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token) && _jwtService.ValidateToken(token, out var claimsPrincipal))
            {
                var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserID");
                if (userIdClaim != null)
                {
                    ViewBag.UserId = userIdClaim.Value;
                    base.OnActionExecuting(context);
                    return;
                }
            }

            // If no valid token or no UserID claim, redirect to login
            context.Result = RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> GetAllPermissionsByUsertId()
        {
            int id = 0;
            if (ViewBag.UserId != null)
                id = Convert.ToInt32(ViewBag.UserId);

            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}common?UserId={id}");

            var Data = await response.Content.ReadAsStringAsync();
            MenuPermissionsDTO menuPermissionsDTO = new MenuPermissionsDTO();
            menuPermissionsDTO = JsonConvert.DeserializeObject<MenuPermissionsDTO>(Data);

            return PartialView("_ManageMenu", menuPermissionsDTO);
        }

        public async Task<JsonResult> GetUserName()
        {
            HttpClient client = new HttpClient();
            int userId = Convert.ToInt32(ViewBag.UserId);
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}users/{userId}");
            var apiResponse = await response.Content.ReadAsStringAsync();

            var resault = JsonConvert.DeserializeObject<UserDTO>(apiResponse);

            return Json(resault.FirstName + " " + resault.LastName);
        }
    }
}
