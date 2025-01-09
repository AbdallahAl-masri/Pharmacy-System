using Common;
using Infrastructure.Base;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.IRepository;
using Service.Implementations;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobDescriptionRepository _jobDescriptionRepository;
        private readonly IErrorLogService _errorLogService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IJwtService _jwtService;
        private readonly ISessionService _sessionService;

        public UserController(IUserRepository userRepository, IJobDescriptionRepository jobDescriptionRepository, IErrorLogService errorLogService,
             IDepartmentRepository departmentRepository, ISectionRepository sectionRepository,
             IJwtService jwtService, ISessionService sessionService)
        {
            _userRepository = userRepository;
            _jobDescriptionRepository = jobDescriptionRepository;
            _errorLogService = errorLogService;
            _departmentRepository = departmentRepository;
            _sectionRepository = sectionRepository;
            _jwtService = jwtService;
            _sessionService = sessionService;
        }


        [HttpGet]
        public IActionResult GetUsers()
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
                _errorLogService.AddErrorLog(ex, "User Controller - GetAllUsers");
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{UserId}")]
        public IActionResult GetUser(int UserId)
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
                             UserName = obj.UserName,
                             Password = obj.Password,

                         }).FirstOrDefault();

                string JsonString = JsonConvert.SerializeObject(users, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "User Controller - GetUserById");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddUser(UserDTO userDTO)
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
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "User Controller - AddNewUser");
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public IActionResult UpdateUser(UserDTO userDTO)
        {
            try
            {
                userDTO.Password = Security.EncryptString(userDTO.Password);
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
                user.Password = userDTO.Password;
                user.UserName = userDTO.UserName;

                _userRepository.Update(user);
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "User Controller - UpdateUser");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("job")]
        public IActionResult GetJobDescriptions()
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
                _errorLogService.AddErrorLog(ex, "User Controller - GetAllJobDescriptions");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult Delete(int Id)
        {

            try
            {
                EntitiyComponent.DBEntities.User obj = new EntitiyComponent.DBEntities.User();
                obj = _userRepository.Find(x => x.UserId == Id).FirstOrDefault();

                _userRepository.Delete(obj);

                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "User Controller - Delete");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = _userRepository.Find(x => x.UserName.Equals(loginDTO.UserName)).FirstOrDefault();

            UserDTO userDTO = new UserDTO()
            {
                UserName = user.UserName,
                UserId = user.UserId,
            };

            if (user == null)
            {
                return Unauthorized();
            }

            SessionInfoDTO sessionDTO = new SessionInfoDTO()
            { 
                UserId = user.UserId.ToString(),
                Token = loginDTO.Token,
            };

            if (await _sessionService.IsSessionActiveAsync(sessionDTO))
            {
                return Conflict(new { message = "This user already has an active session." });
            }


            loginDTO.Password = Security.EncryptString(loginDTO.Password);
            if (user.Password.Equals(loginDTO.Password))
            {
                var token = _jwtService.GenerateToken(userDTO.UserId.ToString(),
                    userDTO.UserName,
                    DateTime.Now.AddMinutes(5));

                return Ok(new { token });
            }

            return Unauthorized();

        }


        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            try
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
            catch (Exception ex)
            {

                _errorLogService.AddErrorLog(ex, "User Controller - GetAllDepartments");
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("sections")]
        public IActionResult GetAllSectionsByDepartmentId(int DepartmentId)
        {
            try
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
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "User Controller - GetAllSectionsByDepartmentId");
                return BadRequest(ex.Message);
            }

        }
    }
}
