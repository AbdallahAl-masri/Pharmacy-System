using API.Services;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.IRepository;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController : Controller
    {
        private readonly IAssignUsersToRoleRepository _assignUsersToRoleRepository;
        private readonly IModuleRoleRepository _moduleRoleRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IErrorLogService _errorLogService;

        public CommonController(IAssignUsersToRoleRepository assignUsersToRoleRepository,
            IModuleRoleRepository moduleRoleRepository,
            IModuleRepository moduleRepository,
            IErrorLogService errorLogService)
        {
            _assignUsersToRoleRepository = assignUsersToRoleRepository;
            _moduleRoleRepository = moduleRoleRepository;
            _moduleRepository = moduleRepository;
            _errorLogService = errorLogService;
        }

        public IActionResult GetAllPermissionsByUsertId(int UserId)
        {
            try
            {
                List<int> RolesList = _assignUsersToRoleRepository.Find(x => x.UserId == UserId).Select(x => x.RoleId).ToList();
                List<int> ModulesList = _moduleRoleRepository.Find(x => RolesList.Contains(x.RoleId)).Select(x => x.ModuleId).Distinct().ToList();

                MenuPermissionsDTO menuPermissionsDTO = new MenuPermissionsDTO();

                if (ModulesList.Contains(1))
                    menuPermissionsDTO.User = "True";
                if (ModulesList.Contains(2))
                    menuPermissionsDTO.Medicine = "True";
                if (ModulesList.Contains(3))
                    menuPermissionsDTO.Store = "True";

                string JsonString = JsonConvert.SerializeObject(menuPermissionsDTO, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Common Controller - GetAllPermissionsByUsertId");
                return BadRequest(ex.Message);
            }

        }

        public IActionResult GetAllServices(string Key)
        {

            try
            {
                List<ServicesDTO> servicesDTOs = (from x in _moduleRepository.Find(x => x.ModuleName.Contains(Key))
                                                  select new ServicesDTO
                                                  {
                                                      Name = x.ModuleName,
                                                      URL = x.MuduleUrl,
                                                  }).ToList();

                string jsonString = JsonConvert.SerializeObject(servicesDTOs, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(jsonString);
            }
            catch (Exception ex)
            {

                _errorLogService.AddErrorLog(ex, "Common Controller - GetAllServices");
                return BadRequest(ex.Message);
            }
        }
    }
}
