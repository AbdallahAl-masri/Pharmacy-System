using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace UI.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Create()
        {
            var ReferenceNumber = Guid.NewGuid().ToString("N").ToUpper();
            ViewBag.ReferenceNumber = ReferenceNumber;
            return View();
        }

        public async Task<IActionResult> AddNewInvoice(InvoiceDTO invoiceDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(invoiceDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "Invoice/AddNewInvoice", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllInvoices");

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<JsonResult> SearchMedicines(string query)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/SearchMedicines?key=" + query);
            var apiReesponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SearchMedicineDTO>>(apiReesponse);

            return Json(result);
        }

        public async Task<IActionResult> GetAllInvoices()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Invoice/GetAllInvoices");
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<InvoiceDTO>>(apiResponse);

                return View(result);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }
    }
}
