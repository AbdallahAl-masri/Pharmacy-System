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

            var apiResponse = await response.Content.ReadAsStringAsync();

            ViewBag.MedicineDepartment = JsonConvert.DeserializeObject<List<MedicineDepartmentDTO>>(apiResponse);

            return View();
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

            return RedirectToAction("GetAllMedicines");
        }

        public async Task<IActionResult> GetAllMedicines()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "Medicine/GetAllMedicine");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<MedicinesDTO>>(apiResponse);

            return View(result);
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
