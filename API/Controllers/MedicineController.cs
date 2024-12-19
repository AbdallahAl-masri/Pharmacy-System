using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.IRepository;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicineDepartmentRepository _medicineDepartmentRepository;

        public MedicineController(IMedicineRepository medicineRepository, IMedicineDepartmentRepository medicineDepartmentRepository)
        {
            _medicineRepository = medicineRepository;
            _medicineDepartmentRepository = medicineDepartmentRepository;
        }

        public IActionResult AddNewMedicine(MedicinesDTO medicinesDTO)
        {
            try
            {
                Medicine obj = new Medicine();

                obj.MedicineName = medicinesDTO.MedicineName;
                obj.MedicineDepartmentId = medicinesDTO.MedicineDepartmentId;
                obj.Description = medicinesDTO.Description;
                obj.ImageReadPath = medicinesDTO.ImageReadPath;
                obj.ImageFullPath = medicinesDTO.ImageFullPath;
                obj.ImageName = medicinesDTO.ImageName;

                _medicineRepository.Add(obj);
                return Ok("Succes");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetAllMedicineDepartments()
        {
            try
            {
                List<MedicineDepartmentDTO> medicineDepartmentDTOs = new List<MedicineDepartmentDTO>();

                medicineDepartmentDTOs = (from obj in _medicineDepartmentRepository.GetAll()
                                          select new MedicineDepartmentDTO
                                          {
                                              Id = obj.MedicineDepartmentId,
                                              Name = obj.DepartmentName,
                                          }).ToList();

                string JsonString = JsonConvert.SerializeObject(medicineDepartmentDTOs, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetAllMedicine()
        {
            try
            {
                List<MedicinesDTO> medicinesDTOs = new List<MedicinesDTO>();

                medicinesDTOs = (from obj in _medicineRepository.GetAll()
                                 select new MedicinesDTO
                                 {
                                     MedicineId = obj.MedicineId,
                                     MedicineName = obj.MedicineName,
                                     DepartmentName = obj.MedicineDepartment.DepartmentName,
                                     
                                 }).ToList();

                string JsonString = JsonConvert.SerializeObject(medicinesDTOs, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult SearchMedicines(string key)
        {
            try
            {
                var medicines = _medicineRepository
                .GetAll()
                .Where(m => EF.Functions.Like(m.MedicineName, $"%{key}%"))
                .Select(m => new SearchMedicineDTO
                {
                    MedicineName = m.MedicineName,
                    MedicineId = m.MedicineId,
                })
                .Take(10) // Limit results for performance
                .ToList();


                //string JsonString = JsonConvert.SerializeObject(medicines, Formatting.None, new JsonSerializerSettings
                //{
                //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //});

                return Ok(medicines);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
