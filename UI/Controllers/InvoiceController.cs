using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
