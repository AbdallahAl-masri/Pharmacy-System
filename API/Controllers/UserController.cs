using Hope.Repository.IRepository;
using Hope.Repository.Repository;
using Infrastructure.Base;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.IRepository;
using System.Diagnostics;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobDescriptionRepository _jobDescriptionRepository;
        private readonly IErrorLogRepository _errorRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISectionRepository _sectionRepository;

        public UserController(IUserRepository userRepository, IJobDescriptionRepository jobDescriptionRepository, IErrorLogRepository errorLogRepository,
             IDepartmentRepository departmentRepository ,ISectionRepository sectionRepository)
        {
            _userRepository = userRepository;
            _jobDescriptionRepository = jobDescriptionRepository;
            _errorRepository = errorLogRepository;
            _departmentRepository = departmentRepository;
            _sectionRepository = sectionRepository;
        }

        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserDTO> users = new List<UserDTO>();
                users = (from obj in _userRepository.Find(x => x.UserId != 0, x => x.JobDescription)
                         select new UserDTO
                         {
                             UserId = obj.UserId,
                             FirstName = obj.FirstName,
                             LastName = obj.LastName,
                             Email = obj.Email,
                             Mobilenumber = obj.Mobilenumber,
                             JoinDate = obj.JoinDate,
                             Address = obj.Address,
                             DateOfBirth = obj.DateOfBirth,
                             Salary = obj.Salary,
                             ShiftTypeName = obj.ShiftType == false ? "Shift A" : "Shift B",
                             GenderDisplayName = obj.Gender == false ? "Male" : "Female",
                             JobDescriptionName = obj.JobDescription.Name,
                             IsActive = obj.IsActive,

                         }).ToList();

                string JsonString = JsonConvert.SerializeObject(users, Formatting.None, new JsonSerializerSettings
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

        public IActionResult GetUserById(int UserId)
        {
            try
            {
                UserDTO users = new UserDTO();
                users = (from obj in _userRepository.Find(x => x.UserId == UserId)
                         select new UserDTO
                         {
                             UserId = obj.UserId,
                             FirstName = obj.FirstName,
                             LastName = obj.LastName,
                             Email = obj.Email,
                             Mobilenumber = obj.Mobilenumber,
                             JoinDate = obj.JoinDate,
                             Address = obj.Address,
                             DateOfBirth = obj.DateOfBirth,
                             Salary = obj.Salary,
                             ShiftType = obj.ShiftType,
                             Gender = obj.Gender,
                             JobDescriptionId = obj.JobDescriptionId,
                             IsActive = obj.IsActive,

                         }).FirstOrDefault();

                users.JobDescriptionsList = new List<JobDescriptionDTO>();
                users.JobDescriptionsList = (from obj in _jobDescriptionRepository.GetAll()
                                             select new JobDescriptionDTO
                                             {
                                                 Id = obj.JobDescriptonId,
                                                 Name = obj.Name
                                             }).ToList();

                string JsonString = JsonConvert.SerializeObject(users, Formatting.None, new JsonSerializerSettings
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

        public IActionResult AddNewUser(UserDTO userDTO)
        {
            try
            {
                userDTO.Password = Security.EncryptString(userDTO.Password); 
                EntitiyComponent.DBEntities.User user = new EntitiyComponent.DBEntities.User();

                user.Address = userDTO.Address;
                user.Email = userDTO.Email;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Gender = userDTO.Gender;
                user.Mobilenumber = userDTO.Mobilenumber;
                user.DateOfBirth = userDTO.DateOfBirth;
                user.IsActive = true;
                user.JoinDate = userDTO.JoinDate;
                user.Salary = userDTO.Salary;
                user.ShiftType = userDTO.ShiftType;
                user.JobDescriptionId = userDTO.JobDescriptionId;
                user.UserName = userDTO.UserName;
                user.Password = userDTO.Password;
                user.DeparmentId = userDTO.DepartmentId;
                user.SectionId = userDTO.SectionId;

                _userRepository.Add(user);
                return Ok("Success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        public IActionResult UpdateUser(UserDTO userDTO)
        {
            try
            {
                EntitiyComponent.DBEntities.User user = new EntitiyComponent.DBEntities.User();

                user = _userRepository.Find(x => x.UserId == userDTO.UserId).FirstOrDefault();

                user.Address = userDTO.Address;
                user.Email = userDTO.Email;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Gender = userDTO.Gender;
                user.Mobilenumber = userDTO.Mobilenumber;
                user.DateOfBirth = userDTO.DateOfBirth;
                user.IsActive = true;
                user.JoinDate = userDTO.JoinDate;
                user.Salary = userDTO.Salary;
                user.ShiftType = userDTO.ShiftType;
                user.JobDescriptionId = userDTO.JobDescriptionId;

                _userRepository.Update(user);
                return Ok("Success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetAllJobDescriptions()
        {
            try
            {
                List<JobDescriptionDTO> jobDescriptions = new List<JobDescriptionDTO>();

                jobDescriptions = (from obj in _jobDescriptionRepository.GetAll()
                                   select new JobDescriptionDTO
                                   {
                                       Id = obj.JobDescriptonId,
                                       Name = obj.Name
                                   }).ToList();

                string JsonString = JsonConvert.SerializeObject(jobDescriptions, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                try
                {
                    EntitiyComponent.DBEntities.ErrorLog obj = new EntitiyComponent.DBEntities.ErrorLog();
                    obj.ErrorExeption = ex.InnerException != null ? ex.InnerException.ToString() : "";
                    obj.ErrorMessage = ex.Message != null ? ex.Message.ToString() : "";
                    obj.ModuleName = "User - GetAllJobDescriptions";
                    obj.TransactionDate = DateTime.Now;
                    _errorRepository.Add(obj);
                }
                catch (Exception)
                {
                    var appLog = new EventLog("Application");
                    appLog.Source = "Application";
                    appLog.WriteEntry("Error Occured in Pharmacy Project", EventLogEntryType.Error);
                }
                
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Delete(int Id)
        {

            try
            {
                EntitiyComponent.DBEntities.User obj = new EntitiyComponent.DBEntities.User();
                obj = _userRepository.Find(x => x.UserId == Id).FirstOrDefault();

                _userRepository.Delete(obj);

                return Ok("Success");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Login(LoginDTO loginDTO)
        {
            var result = _userRepository.Find(x => x.UserName.Equals(loginDTO.UserName)).FirstOrDefault();

            if (result == null)
            {
                return BadRequest(-1);
            }
            else
            {
                loginDTO.Password = Security.EncryptString(loginDTO.Password);
                if(result.Password.Equals(loginDTO.Password))
                {
                    return Ok(result.UserId);
                }
                else
                {
                    return BadRequest(-1);
                }
            }
        }

        public IActionResult GetAllDepartments()
        {
            List<DepartmentDTO> lst = new List<DepartmentDTO>();

            lst = (from obj in _departmentRepository.GetAll()
                   select new DepartmentDTO
                   {
                       DepartmentId = obj.DepartmentId,
                       DepartmentName = obj.Name,
                   }).ToList();

            string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Ok(jsonString);
        }

        public IActionResult GetAllSectionsByDepartmentId(int DepartmentId)
        {
            List<SectionDTO> lst = new List<SectionDTO>();

            lst = (from obj in _sectionRepository.Find(x => x.DepartmentId == DepartmentId)
                   select new SectionDTO
                   {
                       SectionId = obj.SectionId,
                       SectionName = obj.SectionName,
                   }).ToList();

            string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Ok(jsonString);
        }
    }
}
