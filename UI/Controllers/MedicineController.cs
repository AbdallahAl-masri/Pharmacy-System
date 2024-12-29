using Azure;
using Infrastructure.Base;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UI.Controllers
{
    public class MedicineController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicineDepartments");
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                ViewBag.MedicineDepartment = JsonConvert.DeserializeObject<List<MedicineDepartmentDTO>>(apiResponse);

                return View();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> AddNewMedicine(MedicineImage medicineImageDTO)
        {
            MedicinesDTO medicinesDTO = new MedicinesDTO();
            if (medicineImageDTO.Image != null)
            {
                string fileName = Path.GetFileName(medicineImageDTO.Image.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                string ReadPath = "http://localhost:5130/" + "images/" + fileName;
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    medicineImageDTO.Image.CopyTo(stream);
                }

                medicinesDTO.ImageFullPath = uploadPath;
                medicinesDTO.ImageName = fileName;
                medicinesDTO.ImageReadPath = ReadPath;

            }

            medicinesDTO.MedicineName = medicineImageDTO.MedicineName;
            medicinesDTO.MedicineDepartmentId = medicineImageDTO.MedicineDepartmentId;
            medicinesDTO.Description = medicineImageDTO.Description;

            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(medicinesDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "Medicine/AddNewMedicine", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllMedicines");

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> GetAllMedicines()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponse);

                return View(result);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();
            var responseDept = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicineDepartments");
            System.Net.HttpStatusCode statusCodeDept = responseDept.StatusCode;

            if (responseDept.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseDept = await responseDept.Content.ReadAsStringAsync();

                ViewBag.MedicineDepartment = JsonConvert.DeserializeObject<List<MedicineDepartmentDTO>>(apiResponseDept); string apiResponse = await responseDept.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeDept });
            }

            var responseMedicine = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetMedicineById?medicineId=" + Id);
            System.Net.HttpStatusCode statusCodeMedicine = responseMedicine.StatusCode;

            if (responseMedicine.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();

                MedicineImage medicine = JsonConvert.DeserializeObject<MedicineImage>(apiResponseMedicine);

                return View(medicine);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeMedicine });
            }

        }

        public async Task<IActionResult> UpdateMedicine(MedicineImage medicineImageDTO)
        {
            MedicinesDTO medicinesDTO = new MedicinesDTO();
            if (medicineImageDTO.Image != null)
            {
                string fileName = Path.GetFileName(medicineImageDTO.Image.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                string ReadPath = "http://localhost:5130/" + "images/" + fileName;
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    medicineImageDTO.Image.CopyTo(stream);
                }

                medicinesDTO.ImageFullPath = uploadPath;
                medicinesDTO.ImageName = fileName;
                medicinesDTO.ImageReadPath = ReadPath;

            }
            medicinesDTO.MedicineId = medicineImageDTO.MedicineId;
            medicinesDTO.MedicineName = medicineImageDTO.MedicineName;
            medicinesDTO.MedicineDepartmentId = medicineImageDTO.MedicineDepartmentId;
            medicinesDTO.Description = medicineImageDTO.Description;
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(medicinesDTO);
            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "Medicine/UpdateMedicine", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllMedicines");

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }
            
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ConfigSettings.BaseApiUrl + "Medicine/DeleteMedicine?medicineId=" + Id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllMedicines");
            }
            else
            {
                return View();
            }
        }

        public class MedicineImage
        {
            public int MedicineId { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            public string MedicineName { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            public int MedicineDepartmentId { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
            public string Description { get; set; }

            public IFormFile Image { get; set; }
        }
    }
}
