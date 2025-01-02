using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Infrastructure.Base
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsUserLoggedIn())
            {
                ViewBag.UserId = HttpContext.User.Claims.Where(x => x.Type == "UserID").FirstOrDefault().Value;
            }
            else
            {
                context.Result = RedirectToAction("Login", "Account");
            }
        }

        private bool IsUserLoggedIn()
        {
            var result = HttpContext.User.Claims.Where(x => x.Type == "UserID").FirstOrDefault();

            if (result != null)
            {
                return true;
            }
            return false;
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
