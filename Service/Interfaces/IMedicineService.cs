using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMedicineService
    {
        public Task<HttpResponseMessage> GetAllMedicineDepartments();
        public Task<HttpResponseMessage> AddNewMedicine(MedicineDTO medicineDTO);
        public Task<HttpResponseMessage> GetAllMedicines();
        public Task<HttpResponseMessage> GetMedicineById(int Id);
        public Task<HttpResponseMessage> UpdateMedicine(MedicineDTO medicineDTO);
        public Task<HttpResponseMessage> DeleteMedicine(int Id);
    }
}
