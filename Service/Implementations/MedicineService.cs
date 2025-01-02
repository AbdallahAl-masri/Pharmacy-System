using Infrastructure.DTO;
using Infrastructure.Helper;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class MedicineService : IMedicineService
    {
        public async Task<HttpResponseMessage> GetAllMedicineDepartments()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "medicine/departments");

            return response;
        }

        public async Task<HttpResponseMessage> AddNewMedicine(MedicineDTO medicineDTO)
        {
            HttpClient client = new HttpClient();
            var ClientContextDTO = JsonConvert.SerializeObject(medicineDTO);

            var response = await client.PostAsync(ConfigSettings.BaseApiUrl + "medicine", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> GetAllMedicines()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(ConfigSettings.BaseApiUrl + "medicine");

            return response;
        }

        public async Task<HttpResponseMessage> GetMedicineById(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{ConfigSettings.BaseApiUrl}medicine/{Id}");

            return response;
        }

        public async Task<HttpResponseMessage> UpdateMedicine(MedicineDTO medicineDTO)
        {
            HttpClient client = new HttpClient();
            var clientContextDTO = JsonConvert.SerializeObject(medicineDTO);
            var response = await client.PutAsync(ConfigSettings.BaseApiUrl + "medicine", new StringContent(clientContextDTO, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> DeleteMedicine(int Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(ConfigSettings.BaseApiUrl + "medicine?medicineId=" + Id);

            return response;

        }
    }
}
