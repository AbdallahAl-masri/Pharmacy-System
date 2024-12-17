﻿using Hope.Repository.IRepository;
using Infrastructure.DTO;
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

        public CommonController(IAssignUsersToRoleRepository assignUsersToRoleRepository, IModuleRoleRepository moduleRoleRepository ,IModuleRepository moduleRepository)
        {
            _assignUsersToRoleRepository = assignUsersToRoleRepository;
            _moduleRoleRepository = moduleRoleRepository;
            _moduleRepository = moduleRepository;
        }

        public IActionResult GetAllPermissionsByUsertId(int UserId)
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

        public IActionResult GetAllServices(string Key)
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
    }
}