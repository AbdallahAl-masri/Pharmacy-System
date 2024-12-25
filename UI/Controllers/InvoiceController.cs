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

            return RedirectToAction("GetAllInvoices");
        }

        public async Task<JsonResult> SearchMedicines(string query)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/SearchMedicines?key=" + query);
            var apiReesponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SearchMedicineDTO>>(apiReesponse);

            return Json(result);
        }

        public async Task<IActionResult> GetAllInvoices()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Invoice/GetAllInvoices");
            string apiResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<InvoiceDTO>>(apiResponse);

            return View(result);
        }
    }
}
