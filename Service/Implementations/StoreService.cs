﻿using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class StoreService : IStoreService
    {
        public async Task<HttpResponseMessage> GetAllSupplier()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetAllSupplier");

            return response;
        }

        public async Task<HttpResponseMessage> GetAllMedicine()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");

            return response;
        }

        public async Task<HttpResponseMessage> AddNewStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(storeDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "Store/AddNewStore", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllStores()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetAllStores");

            return response;
        }

        public async Task<HttpResponseMessage> GetStoreById(int Id)
        {
            HttpClient client = new HttpClient();
            var responseStore = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetStoreById?StoreId=" + Id);

            return responseStore;
        }

        public async Task<HttpResponseMessage> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ConfigSettings.BaseApiUrl + "Store/Delete?StoreId=" + Id);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var ClientContextDTO = JsonConvert.SerializeObject(storeDTO);

            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "Store/UpdateStore", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}
