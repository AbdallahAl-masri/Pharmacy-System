using Azure;
using Infrastructure.Base;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace UI.Controllers
{
    public class UserController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            var responseJob = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllJobDescriptions");
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


            var responseDept = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllDepartments");
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
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllSectionsByDepartmentId?DepartmentId=" + DeptId);

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
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "User/AddNewUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            System.Net.HttpStatusCode statusCode = response.StatusCode;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllUsers");

            }
            else
            {
                return RedirectToAction("Error", "Home", new { code = (int)statusCode });
            }

        }

        public async Task<IActionResult> GetAllUsers()
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllUsers");
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
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}User/Delete?id={Id}");

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
            HttpClient client = new HttpClient();
            var responseJob = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllJobDescriptions");
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


            var responseDept = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllDepartments");
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

            var responseUser = await client.GetAsync($"{ConfigSettings.BaseApiUrl}User/GetUserById?UserId={Id}");
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
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "User/UpdateUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));
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
