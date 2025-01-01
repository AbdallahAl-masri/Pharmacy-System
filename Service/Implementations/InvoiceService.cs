using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        public async Task<HttpResponseMessage> AddNewInvoice(InvoiceDTO invoiceDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(invoiceDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "Invoice/AddNewInvoice", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllInvoices()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Invoice/GetAllInvoices");

            return response;
        }

        public async Task<HttpResponseMessage> SearchMedicines(string query)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/SearchMedicines?key=" + query);

            return response;
        }
    }
}
