using Infrastructure.Base;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await _dashboardService.GetDashboardDetails();
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                DashboardDTO dashboardDTO = JsonConvert.DeserializeObject<DashboardDTO>(apiResponse);

                ViewBag.SupplierCount = dashboardDTO.SupplierCount;
                ViewBag.MedicineCount = dashboardDTO.MedicineCount;


                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? code)
        {
            if (code.HasValue)
            {
                ViewData["ErrorCode"] = code;
            }
            else
            {
                ViewData["ErrorCode"] = "Unknown Error";
            }
            return View();
        }
    }
}
