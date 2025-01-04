//using Azure.Core;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Service.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        public async Task<HttpResponseMessage> GetAllJobDescriptions()
        {
            HttpClient client = new HttpClient();
            var responseJob = await client.GetAsync(ConfigSettings.BaseApiUrl + "users/job");

            return responseJob;
        }

        public async Task<HttpResponseMessage> GetAllDepartments()
        {
            HttpClient client = new HttpClient();
            var responseDept = await client.GetAsync(ConfigSettings.BaseApiUrl + "users/departments");

            return responseDept;
        }

        public async Task<HttpResponseMessage> GetAllSectionsByDepartmentId(string DeptId)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "users/sections?DepartmentId=" + DeptId);

            return response;
        }

        public async Task<HttpResponseMessage> AddNewUser(UserDTO userDTO)
        {
            HttpClient client = new HttpClient();
            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "users", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllUsers()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "users");

            return response;
        }

        public async Task<HttpResponseMessage> GetUserById(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}users/{Id}");

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync($"{ConfigSettings.BaseApiUrl}users?id={Id}");

            return response;
        }

        public async Task<HttpResponseMessage> UpdateUser(UserDTO userDTO)
        {
            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "users", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            HttpClient client = new HttpClient();

            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "users/login", new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));

            var apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(apiResponse);

            return result.token;
        }
    }
}
