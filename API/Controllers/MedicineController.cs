using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.IRepository;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/medicine")]
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

        [HttpPost]
        public IActionResult AddMedicine(MedicineDTO medicinesDTO)
        {
            try
            {
                Medicine obj = new Medicine();

                obj.MedicineName = medicinesDTO.MedicineName;
                obj.MedicineDepartmentId = medicinesDTO.MedicineDepartmentId;
                obj.Description = medicinesDTO.Description;

                _medicineRepository.Add(obj);
                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - AddNewMedicine");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("departments")]
        public IActionResult GetMedicineDepartments()
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


        [HttpGet]
        public IActionResult GetMedicines()
        {
            try
            {
                List<MedicineDTO> medicinesDTOs = new List<MedicineDTO>();

                medicinesDTOs = (from obj in _medicineRepository.GetAll()
                                 select new MedicineDTO
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


        [HttpGet("{medicineId}")]
        public IActionResult GetMedicine(int medicineId)
        {
            try
            {

                MedicineDTO medicine = new MedicineDTO();
                medicine = (from m in _medicineRepository.Find(m => m.MedicineId == medicineId)
                            select new MedicineDTO
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


        [HttpPut]
        public IActionResult UpdateMedicine(MedicineDTO medicinesDTO)
        {
            try
            {
                Medicine medicine = new Medicine();
                medicine = _medicineRepository.Find(m => m.MedicineId == medicinesDTO.MedicineId).FirstOrDefault();

                medicine.MedicineName = medicinesDTO.MedicineName;
                medicine.Description = medicinesDTO.Description;
                medicine.MedicineDepartmentId = medicinesDTO.MedicineDepartmentId;

                _medicineRepository.Update(medicine);
                return Ok();

            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Medicine Controller - UpdateMedicine");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
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
