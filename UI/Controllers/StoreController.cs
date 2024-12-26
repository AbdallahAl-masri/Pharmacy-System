using Infrastructure.Base;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace UI.Controllers
{
    public class StoreController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            var responseSupplier = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetAllSupplier");
            var apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();
            ViewBag.Suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(apiResponseSupplier);

            var responseMedicine = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");
            var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
            ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponseMedicine);

            return View();
        }

        public async Task<IActionResult> AddNewStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(storeDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "Store/AddNewStore", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return RedirectToAction("GetAllStores");
        }

        public async Task<IActionResult> GetAllStores()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetAllStores");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StoreDTO>>(apiResponse);

            return View(result);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ConfigSettings.BaseApiUrl + "Store/Delete?StoreId=" + Id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllStores");
            }
            else
            {
                return View();
            }


        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            var responseSupplier = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetAllSupplier");
            var apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();
            ViewBag.Suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(apiResponseSupplier);

            var responseMedicine = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");
            var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
            ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponseMedicine);

            var responseStore = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetStoreById?StoreId=" + Id);
            var apiResponseStore = await responseStore.Content.ReadAsStringAsync();

            StoreDTO store = JsonConvert.DeserializeObject<StoreDTO>(apiResponseStore);

            return View(store);
        }

        public async Task<IActionResult> UpdateStore(StoreDTO store)
        {
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(store);

            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "Store/UpdateStore", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return RedirectToAction("GetAllStores");
        }
    }
}
