using Infrastructure.Base;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Dashboard/GetDashboardDetails");

            var apiResponse = await response.Content.ReadAsStringAsync();

            DashboardDTO dashboardDTO = JsonConvert.DeserializeObject<DashboardDTO>(apiResponse);

            ViewBag.SupplierCount = dashboardDTO.SupplierCount;
            ViewBag.MedicineCount = dashboardDTO.MedicineCount;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
