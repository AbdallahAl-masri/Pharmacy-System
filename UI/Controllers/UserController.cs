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
            string apiResponseJob = await responseJob.Content.ReadAsStringAsync();

            var responseDept = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllDepartments");
            string apiResponseDept = await responseDept.Content.ReadAsStringAsync();



            ViewBag.JobDescription = JsonConvert.DeserializeObject<List<JobDescriptionDTO>>(apiResponseJob);
            ViewBag.Department = JsonConvert.DeserializeObject<List<DepartmentDTO>>(apiResponseDept);

            return View();
        }

        public async Task<IActionResult> GetAllSectionByDeptId(string DeptId)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllSectionsByDepartmentId?DepartmentId=" + DeptId);
            string apiResponse = await response.Content.ReadAsStringAsync();

            List<SectionDTO> lst = JsonConvert.DeserializeObject<List<SectionDTO>>(apiResponse);


            return Json(lst);

        }

        public async Task<IActionResult> AddNewUser(UserDTO userDTO)
        {
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "User/AddNewUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return RedirectToAction("GetAllUsers");
        }

        public async Task<IActionResult> GetAllUsers()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllUsers");
            string apiResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<UserDTO>>(apiResponse);

            return View(result);
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
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}User/GetUserById?UserId={Id}");
            var apiResponse = await response.Content.ReadAsStringAsync();
            UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(apiResponse);

            //client = new HttpClient();
            //response = await client.GetAsync("http://localhost:5039/api/User/GetAllJobDescriptions");
            //apiResponse = await response.Content.ReadAsStringAsync();
            //ViewBag.JobDescription = JsonConvert.DeserializeObject<List<JobDescriptionDTO>>(apiResponse);
            ViewBag.JobDescription = userDTO.JobDescriptionsList;

            return View(userDTO);
        }

        public async Task<IActionResult> UpdateUser(UserDTO userDTO)
        {
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "User/UpdateUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return RedirectToAction("GetAllUsers");
        }
    }
}
