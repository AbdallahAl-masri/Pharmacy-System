using Infrastructure.DTO;
using Infrastructure.Helper;
using Newtonsoft.Json;
using Service.Interfaces;
using System.Text;

namespace Service.Implementations
{
    public class StoreService : IStoreService
    {
        public async Task<HttpResponseMessage> GetAllSupplier()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "store/supplier");

            return response;
        }

        public async Task<HttpResponseMessage> GetAllMedicine()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "medicine");

            return response;
        }

        public async Task<HttpResponseMessage> AddNewStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(storeDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "store", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllStores()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "store");

            return response;
        }

        public async Task<HttpResponseMessage> GetStoreById(int Id)
        {
            HttpClient client = new HttpClient();
            var responseStore = await client.GetAsync($"{ConfigSettings.BaseApiUrl}store/{Id}");

            return responseStore;
        }

        public async Task<HttpResponseMessage> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ConfigSettings.BaseApiUrl + "store?StoreId=" + Id);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var ClientContextDTO = JsonConvert.SerializeObject(storeDTO);

            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "store", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}
