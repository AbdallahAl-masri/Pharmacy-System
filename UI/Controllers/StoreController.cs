using Azure;
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
            System.Net.HttpStatusCode statusCodeSupplier = responseSupplier.StatusCode;

            if (responseSupplier.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();
                ViewBag.Suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(apiResponseSupplier); string apiResponse = await responseSupplier.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeSupplier });
            }

            var responseMedicine = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");
            System.Net.HttpStatusCode statusCodeMedicine = responseMedicine.StatusCode;

            if (responseMedicine.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
                ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponseMedicine);

                return View();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeMedicine });
            }

        }

        public async Task<IActionResult> AddNewStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(storeDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "Store/AddNewStore", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllStores");

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> GetAllStores()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetAllStores");
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<StoreDTO>>(apiResponse);

                return View(result);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

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
            System.Net.HttpStatusCode statusCode = responseSupplier.StatusCode;

            if (responseSupplier.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();
                ViewBag.Suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(apiResponseSupplier); string apiResponse = await responseSupplier.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

            var responseMedicine = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");
            statusCode = responseMedicine.StatusCode;

            if (responseMedicine.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
                ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponseMedicine);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

            var responseStore = await client.GetAsync(ConfigSettings.BaseApiUrl + "Store/GetStoreById?StoreId=" + Id);
            statusCode = responseStore.StatusCode;

            if (responseStore.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseStore = await responseStore.Content.ReadAsStringAsync();

                StoreDTO store = JsonConvert.DeserializeObject<StoreDTO>(apiResponseStore);

                return View(store);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> UpdateStore(StoreDTO store)
        {
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(store);

            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "Store/UpdateStore", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllStores");

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }
            
        }
    }
}
