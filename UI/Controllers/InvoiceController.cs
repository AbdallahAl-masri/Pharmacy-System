using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;

namespace UI.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        public IActionResult Create()
        {
            var ReferenceNumber = Guid.NewGuid().ToString("N").ToUpper();
            ViewBag.ReferenceNumber = ReferenceNumber;
            return View();
        }

        public async Task<IActionResult> AddNewInvoice(InvoiceDTO invoiceDTO)
        {
            var response = await _invoiceService.AddNewInvoice(invoiceDTO);
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
            var response = await _invoiceService.SearchMedicines(query);
            var apiReesponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SearchMedicineDTO>>(apiReesponse);

            return Json(result);
        }

        public async Task<IActionResult> GetAllInvoices()
        {
            HttpClient client = new HttpClient();
            var response = await _invoiceService.GetAllInvoices();
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
