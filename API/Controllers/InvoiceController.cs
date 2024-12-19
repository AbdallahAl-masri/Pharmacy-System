using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceMasterRepository _invoiceMasterRepository;
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IStoreRepository _storeRepository;

        public InvoiceController(IInvoiceMasterRepository invoiceMasterRepository, IInvoiceDetailsRepository invoiceDetailsRepository,
            IMedicineRepository medicineRepository, IStoreRepository storeRepository)
        {
            _invoiceMasterRepository = invoiceMasterRepository;
            _invoiceDetailsRepository = invoiceDetailsRepository;
            _medicineRepository = medicineRepository;
            _storeRepository = storeRepository;
        }
        public IActionResult AddNewInvoice(InvoiceDTO invoiceDTO)
        {
            // Create invoice
            var newInvoice = new InvoiceMaster
            {
                ReferenceNumber = Guid.NewGuid().ToString("N").ToUpper(),
                CustomerName = invoiceDTO.CustomerName,
                TransactionDate = DateOnly.FromDateTime(DateTime.Now),
            };

            _invoiceMasterRepository.Add(newInvoice);

            // Add invoice details
            foreach (var detail in invoiceDTO.InvoiceDetails)
            {
                var medicine = _medicineRepository.Find(x => x.MedicineName == detail.MedicineName).FirstOrDefault();
                var descount = _storeRepository.Find(x => x.MedicineId == medicine.MedicineId).FirstOrDefault();
                var totalCost = detail.Quantity * detail.Price - (detail.Price * descount.MaxDiscount);

                var invoiceDetail = new InvoiceDetail
                {
                    InvoiceMasterId = newInvoice.InvoiceMasterId,
                    MedicineId = medicine.MedicineId,
                    Qty = detail.Quantity,
                    SellingPrice = detail.Price,
                    TotalCost = totalCost
                };

                _invoiceDetailsRepository.Add(invoiceDetail);
            }

            return View();
        }
    }
}
