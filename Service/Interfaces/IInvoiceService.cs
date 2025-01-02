using Infrastructure.DTO;

namespace Service.Interfaces
{
    public interface IInvoiceService
    {
        public Task<HttpResponseMessage> AddNewInvoice(InvoiceDTO invoiceDTO);
        public Task<HttpResponseMessage> GetAllInvoices();
        public Task<HttpResponseMessage> SearchMedicines(string query);
    }
}
