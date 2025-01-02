using Infrastructure.DTO;
using Infrastructure.Helper;
using Newtonsoft.Json;
using Service.Interfaces;
using System.Text;

namespace Service.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        public async Task<HttpResponseMessage> AddNewInvoice(InvoiceDTO invoiceDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(invoiceDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "invoice", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllInvoices()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "invoice");

            return response;
        }

        public async Task<HttpResponseMessage> SearchMedicines(string query)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "store/search?key=" + query);

            return response;
        }
    }
}
