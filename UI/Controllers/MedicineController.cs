using Infrastructure.Base;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace UI.Controllers
{
    public class MedicineController : BaseController
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService mediicineService)
        {
            _medicineService = mediicineService;
        }

        public async Task<IActionResult> Create()
        {
            var response = await _medicineService.GetAllMedicineDepartments();
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
            MedicineDTO medicineDTO = new MedicineDTO();
            if (medicineImageDTO.Image != null)
            {
                string fileName = Path.GetFileName(medicineImageDTO.Image.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                string ReadPath = "http://localhost:5130/" + "images/" + fileName;
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    medicineImageDTO.Image.CopyTo(stream);
                }

                medicineDTO.ImageFullPath = uploadPath;
                medicineDTO.ImageName = fileName;
                medicineDTO.ImageReadPath = ReadPath;

            }

            medicineDTO.MedicineName = medicineImageDTO.MedicineName;
            medicineDTO.MedicineDepartmentId = medicineImageDTO.MedicineDepartmentId;
            medicineDTO.Description = medicineImageDTO.Description;

            var response = await _medicineService.AddNewMedicine(medicineDTO);
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
            var response = await _medicineService.GetAllMedicines();
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MedicineDTO>>(apiResponse);

                return View(result);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> Update(int Id)
        {
            var responseDept = await _medicineService.GetAllMedicineDepartments();
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

            var responseMedicine = await _medicineService.GetMedicineById(Id);
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
            MedicineDTO medicineDTO = new MedicineDTO();
            if (medicineImageDTO.Image != null)
            {
                string fileName = Path.GetFileName(medicineImageDTO.Image.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                string ReadPath = "http://localhost:5130/" + "images/" + fileName;
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    medicineImageDTO.Image.CopyTo(stream);
                }

                medicineDTO.ImageFullPath = uploadPath;
                medicineDTO.ImageName = fileName;
                medicineDTO.ImageReadPath = ReadPath;

            }
            medicineDTO.MedicineId = medicineImageDTO.MedicineId;
            medicineDTO.MedicineName = medicineImageDTO.MedicineName;
            medicineDTO.MedicineDepartmentId = medicineImageDTO.MedicineDepartmentId;
            medicineDTO.Description = medicineImageDTO.Description;

            var response = await _medicineService.UpdateMedicine(medicineDTO);
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
            var response = await _medicineService.DeleteMedicine(Id);

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
