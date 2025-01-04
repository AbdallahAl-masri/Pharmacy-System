using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.IRepository;
using Service.Interfaces;
namespace API.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceMasterRepository _invoiceMasterRepository;
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IErrorLogService _errorLogService;

        public InvoiceController(IInvoiceMasterRepository invoiceMasterRepository, IInvoiceDetailsRepository invoiceDetailsRepository,
            IMedicineRepository medicineRepository, IStoreRepository storeRepository,
            IErrorLogService errorLogService)
        {
            _invoiceMasterRepository = invoiceMasterRepository;
            _invoiceDetailsRepository = invoiceDetailsRepository;
            _medicineRepository = medicineRepository;
            _storeRepository = storeRepository;
            _errorLogService = errorLogService;
        }


        [HttpPost]
        public IActionResult AddInvoice(InvoiceDTO invoiceDTO)
        {

            try
            {
                // Create invoice
                var newInvoice = new InvoiceMaster
                {
                    ReferenceNumber = invoiceDTO.ReferenceNumber,
                    CustomerName = invoiceDTO.CustomerName,
                    TransactionDate = DateOnly.FromDateTime(DateTime.Now),
                    GrandTotal = invoiceDTO.GrandTotal,
                };

                // Add the new invoice master to the database
                _invoiceMasterRepository.Add(newInvoice);

                // Add invoice details
                foreach (var detail in invoiceDTO.InvoiceDetails)
                {
                    // Ensure the medicine exists
                    var medicine = _medicineRepository.Find(x => x.MedicineName == detail.MedicineName).FirstOrDefault();

                    // Ensure the discount exists for the medicine
                    var store = _storeRepository.Find(x => x.MedicineId == medicine.MedicineId).FirstOrDefault();

                    var discount = invoiceDTO.Discount;

                    // Calculate total cost
                    var totalCost = detail.TotalPrice - (detail.Price * discount);

                    // Create invoice detail
                    var invoiceDetail = new InvoiceDetail
                    {
                        InvoiceMasterId = newInvoice.InvoiceMasterId,
                        MedicineId = medicine.MedicineId,
                        Qty = detail.Quantity,
                        Price = detail.Price,
                        TotalCost = totalCost
                    };

                    // Add the invoice detail to the database
                    _invoiceDetailsRepository.Add(invoiceDetail);

                    // Update the Quantity of the Medicine
                    store.RemainingQty -= detail.Quantity;
                    _storeRepository.Update(store);
                }


                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Invoice Controller - AddNewInvoice");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IActionResult GetInvoices()
        {
            try
            {
                List<InvoiceDTO> invoices = new List<InvoiceDTO>();
                invoices = (from invoice in _invoiceMasterRepository.GetAll().Include(x => x.InvoiceDetails)
                            select new InvoiceDTO
                            {
                                ReferenceNumber = invoice.ReferenceNumber,
                                TransactionDate = invoice.TransactionDate,
                                CustomerName = invoice.CustomerName,
                                GrandTotal = invoice.GrandTotal,
                                InvoiceDetails = (from detail in invoice.InvoiceDetails
                                                  select new InvoiceDetailDTO
                                                  {
                                                      MedicineName = detail.Medicine.MedicineName,
                                                      Quantity = detail.Qty,
                                                      Price = detail.Price,
                                                      TotalPrice = detail.TotalCost,
                                                  }).ToList()
                            }).ToList();

                string JsonString = JsonConvert.SerializeObject(invoices, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Invoice Controller - GetAllInvoices");
                return BadRequest(ex.Message);
            }
        }
    }
}
