using Azure;
using Infrastructure.Base;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using System.Text;

namespace UI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Create()
        {
            var responseJob = await _userService.GetAllJobDescriptions();
            System.Net.HttpStatusCode statusCodeJob = responseJob.StatusCode;
            string apiResponseJob;
            if (responseJob.StatusCode == System.Net.HttpStatusCode.OK)
            {
                apiResponseJob = await responseJob.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeJob });
            }


            var responseDept = await _userService.GetAllDepartments();
            System.Net.HttpStatusCode statusCodeDept = responseDept.StatusCode;
            string apiResponseDept;
            if (responseDept.StatusCode == System.Net.HttpStatusCode.OK)
            {
                apiResponseDept = await responseDept.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeDept });
            }


            ViewBag.JobDescription = JsonConvert.DeserializeObject<List<JobDescriptionDTO>>(apiResponseJob);
            ViewBag.Department = JsonConvert.DeserializeObject<List<DepartmentDTO>>(apiResponseDept);

            return View();
        }

        public async Task<IActionResult> GetAllSectionByDeptId(string DeptId)
        {
            var response = await _userService.GetAllSectionsByDepartmentId(DeptId);

            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                List<SectionDTO> lst = JsonConvert.DeserializeObject<List<SectionDTO>>(apiResponse);


                return Json(lst);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }


        }

        public async Task<IActionResult> AddNewUser(UserDTO userDTO)
        {

            var response = await _userService.AddNewUser(userDTO);

            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllUsers");

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["ErrorMessage"] = "A user with the same username already exists.";
                return RedirectToAction("Update", new { Id = userDTO.UserId });
            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> GetAllUsers()
        {

            var response = await _userService.GetAllUsers();
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<UserDTO>>(apiResponse);

                return View(result);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _userService.Delete(Id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllUsers");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            var responseJob = await _userService.GetAllJobDescriptions();
            System.Net.HttpStatusCode statusCodeJob = responseJob.StatusCode;
            string apiResponseJob;
            if (responseJob.StatusCode == System.Net.HttpStatusCode.OK)
            {
                apiResponseJob = await responseJob.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeJob });
            }


            var responseDept = await _userService.GetAllDepartments();
            System.Net.HttpStatusCode statusCodeDept = responseDept.StatusCode;
            string apiResponseDept;
            if (responseDept.StatusCode == System.Net.HttpStatusCode.OK)
            {
                apiResponseDept = await responseDept.Content.ReadAsStringAsync();

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeDept });
            }

            var responseUser = await _userService.GetUserById(Id);
            System.Net.HttpStatusCode statusCodeUser = responseUser.StatusCode;
            UserDTO userDTO;
            if (responseUser.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await responseUser.Content.ReadAsStringAsync();
                 userDTO = JsonConvert.DeserializeObject<UserDTO>(apiResponse);

                ViewBag.JobDescription = JsonConvert.DeserializeObject<List<JobDescriptionDTO>>(apiResponseJob);
                ViewBag.Department = JsonConvert.DeserializeObject<List<DepartmentDTO>>(apiResponseDept);

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCodeUser });
            }

            return View(userDTO);

        }

        public async Task<IActionResult> UpdateUser(UserDTO userDTO)
        {

            var response = await _userService.UpdateUser(userDTO);
            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllUsers");

            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["ErrorMessage"] = "A user with the same username already exists.";
                return RedirectToAction("Update", new { Id = userDTO.UserId });
            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }
        }
    }
}
