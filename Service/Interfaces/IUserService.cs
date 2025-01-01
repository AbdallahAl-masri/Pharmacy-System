using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        public Task<HttpResponseMessage> GetAllJobDescriptions();
        public Task<HttpResponseMessage> GetAllDepartments();
        public Task<HttpResponseMessage> GetAllSectionsByDepartmentId(string DeptId);
        public Task<HttpResponseMessage> AddNewUser(UserDTO userDTO);
        public Task<HttpResponseMessage> GetAllUsers();
        public Task<HttpResponseMessage> GetUserById(int Id);
        public Task<HttpResponseMessage> Delete(int Id);
        public Task<HttpResponseMessage> UpdateUser(UserDTO userDTO);
        public Task<HttpResponseMessage> Login(LoginDTO loginDTO);
    }
}
