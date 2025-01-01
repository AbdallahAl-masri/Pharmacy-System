using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IInvoiceService
    {
        public Task<HttpResponseMessage> AddNewInvoice(InvoiceDTO invoiceDTO);
        public Task<HttpResponseMessage> GetAllInvoices();
        public Task<HttpResponseMessage> SearchMedicines(string query);
    }
}
