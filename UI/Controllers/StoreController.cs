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
            var responseSupplier = await client.GetAsync(ConfigSettings.BaseApiUrl+"Store/GetAllSupplier");
            var apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();
            ViewBag.Suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(apiResponseSupplier);

            var responseMedicine = await client.GetAsync(ConfigSettings.BaseApiUrl+"Medicine/GetAllMedicine");
            var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
            ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponseMedicine);

            return View();
        }

        public async Task<IActionResult> AddNewStore(StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(storeDTO);
            var response = await client.PostAsync(ConfigSettings.BaseApiUrl+"Store/AddNewStore", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return View();
        }
    }
}
