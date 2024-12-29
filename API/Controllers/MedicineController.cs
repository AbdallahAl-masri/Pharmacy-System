using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IErrorLogService _errorLogService;

        public MedicineController(IMedicineRepository medicineRepository, IMedicineDepartmentRepository medicineDepartmentRepository,
            IErrorLogService errorLogService)
        {
            _medicineRepository = medicineRepository;
            _medicineDepartmentRepository = medicineDepartmentRepository;
            _errorLogService = errorLogService;
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
                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - AddNewMedicine");
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
                _errorLogService.AddErrorLog(ex, "Medicine Controller - GetAllMedicineDepartments");
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
                                     Description = obj.Description,

                                 }).ToList();

                string JsonString = JsonConvert.SerializeObject(medicinesDTOs, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - GetAllMedicine");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetMedicineById(int medicineId)
        {
            try
            {

                MedicinesDTO medicine = new MedicinesDTO();
                medicine = (from m in _medicineRepository.Find(m => m.MedicineId == medicineId)
                            select new MedicinesDTO
                            {
                                MedicineId = m.MedicineId,
                                MedicineName = m.MedicineName,
                                DepartmentName = m.MedicineDepartment.DepartmentName,
                                Description = m.Description,
                            }).FirstOrDefault();

                string JsonString = JsonConvert.SerializeObject(medicine, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - GetMedicineById");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult UpdateMedicine(MedicinesDTO medicinesDTO)
        {
            try
            {
                Medicine medicine = new Medicine();
                medicine = _medicineRepository.Find(m => m.MedicineId == medicinesDTO.MedicineId).FirstOrDefault();

                medicine.MedicineName = medicinesDTO.MedicineName;
                medicine.Description = medicinesDTO.Description;
                medicine.MedicineDepartmentId = medicinesDTO.MedicineDepartmentId;
                medicine.ImageFullPath = medicinesDTO.ImageFullPath;
                medicine.ImageName = medicinesDTO.ImageName;
                medicine.ImageReadPath = medicinesDTO.ImageReadPath;

                _medicineRepository.Update(medicine);
                return Ok();

            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - UpdateMedicine");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult DeleteMedicine(int medicineId)
        {
            try
            {

                Medicine medicine = new Medicine();
                medicine = _medicineRepository.Find(m => m.MedicineId == medicineId).FirstOrDefault();

                _medicineRepository.Delete(medicine);
                return Ok();

            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - Delete");
                return BadRequest(ex.Message);
            }
        }
    }
}
