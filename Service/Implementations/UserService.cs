﻿using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public  class UserService : IUserService
    {
        public async Task<HttpResponseMessage> GetAllJobDescriptions()
        {
            HttpClient client = new HttpClient();
            var responseJob = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllJobDescriptions");

            return responseJob;
        }

        public async Task<HttpResponseMessage> GetAllDepartments()
        {
            HttpClient client = new HttpClient();
            var responseDept = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllDepartments");

            return responseDept;
        }

        public async Task<HttpResponseMessage> GetAllSectionsByDepartmentId(string DeptId)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllSectionsByDepartmentId?DepartmentId=" + DeptId);

            return response;
        }

        public async Task<HttpResponseMessage> AddNewUser(UserDTO userDTO)
        {
            HttpClient client = new HttpClient();
            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "User/AddNewUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllUsers()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "User/GetAllUsers");

            return response;
        }

        public async Task<HttpResponseMessage> GetUserById(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}User/GetUserById?UserId={Id}");

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}User/Delete?id={Id}");

            return response;
        }

        public async Task<HttpResponseMessage> UpdateUser(UserDTO userDTO)
        {
            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "User/UpdateUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> Login(LoginDTO loginDTO)
        {
            HttpClient client = new HttpClient();
            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "User/Login", new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}
