using Infrastructure.Base;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;

namespace UI.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IActionResult> Create()
        {
            var responseSupplier = await _storeService.GetAllSupplier();
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

            var responseMedicine = await _storeService.GetAllMedicine();
            System.Net.HttpStatusCode statusCodeMedicine = responseMedicine.StatusCode;

            if (responseMedicine.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
                ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicineDTO>>(apiResponseMedicine);

                return View();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeMedicine });
            }

        }

        public async Task<IActionResult> AddNewStore(StoreDTO storeDTO)
        {
            var response = await _storeService.AddNewStore(storeDTO);
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
            var response = await _storeService.GetAllStores();
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
            var response = await _storeService.Delete(Id);

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
            var responseSupplier = await _storeService.GetAllSupplier();
            System.Net.HttpStatusCode statusCode = responseSupplier.StatusCode;

            if (responseSupplier.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();
                ViewBag.Suppliers = JsonConvert.DeserializeObject<List<SupplierDTO>>(apiResponseSupplier);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

            var responseMedicine = await _storeService.GetAllMedicine();
            statusCode = responseMedicine.StatusCode;

            if (responseMedicine.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();
                ViewBag.Medicines = JsonConvert.DeserializeObject<List<MedicineDTO>>(apiResponseMedicine);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

            var responseStore = await _storeService.GetStoreById(Id);
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

            var response = await _storeService.UpdateStore(store);
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
