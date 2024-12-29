using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.IRepository;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IErrorLogService _errorLogService;

        public StoreController(IStoreRepository storeRepository, ISupplierRepository supplierRepository,
            IErrorLogService errorLogService)
        {
            _storeRepository = storeRepository;
            _supplierRepository = supplierRepository;
            _errorLogService = errorLogService;
        }

        public IActionResult AddNewStore(StoreDTO storeDTO)
        {

            try
            {
                Store store = new Store();

                store.ProductionDate = storeDTO.ProductionDate;
                store.CostPrice = storeDTO.CostPrice;
                store.SellingPriceAfterTax = storeDTO.SellingPriceAfterTax;
                store.OriginalQty = storeDTO.OriginalQty;
                store.RemainingQty = storeDTO.RemainingQty;
                store.SellingPriceBeforeTax = storeDTO.SellingPriceBeforeTax;
                store.SupplerId = storeDTO.SupplerId;
                store.MaxDiscount = storeDTO.MaxDiscount;
                store.MedicineId = storeDTO.MedicineId;
                store.ExpiaryDate = storeDTO.ExpiaryDate;
                store.TaxValue = storeDTO.TaxValue;

                _storeRepository.Add(store);

                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - AddNewStore");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetAllStores()
        {
            try
            {
                List<StoreDTO> stores = new List<StoreDTO>();

                stores = (from obj in _storeRepository.GetAll()
                          select new StoreDTO
                          {
                              StoreId = obj.StoreId,
                              CostPrice = obj.CostPrice,
                              OriginalQty = obj.OriginalQty,
                              MedicineDescription = obj.Medicine.Description,
                              DepartmentName = obj.Medicine.MedicineDepartment.DepartmentName,
                              MedicineName = obj.Medicine.MedicineName,
                              RemainingQty = obj.RemainingQty,
                              ExpiaryDate = obj.ExpiaryDate,

                          }).ToList();
                string JsonString = JsonConvert.SerializeObject(stores, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - GetAllStores");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetAllSupplier()
        {
            try
            {
                List<SupplierDTO> supplierDTOs = new List<SupplierDTO>();

                supplierDTOs = (from obj in _supplierRepository.GetAll()
                                select new SupplierDTO
                                {
                                    Id = obj.SupplierId,
                                    Name = obj.SupplierName,
                                }).ToList();
                string JsonString = JsonConvert.SerializeObject(supplierDTOs, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - GetAllSupplier");
                return BadRequest(ex.Message);
            }

        }

        public IActionResult SearchMedicines(string key)
        {

            try
            {
                List<SearchMedicineDTO> medicines = new List<SearchMedicineDTO>();
                medicines = (from m in _storeRepository.GetAll()
                             .Include(m => m.Medicine).Where(m => EF.Functions.Like(m.Medicine.MedicineName, $"%{key}%"))
                             select new SearchMedicineDTO
                             {
                                 MedicineId = m.MedicineId,
                                 MedicineName = m.Medicine.MedicineName,
                                 MedicinePrice = m.SellingPriceAfterTax,
                                 MedicineQTY = m.RemainingQty,
                                 Discount = m.MaxDiscount,
                             })
                             .Take(10) // Limit results for performance
                             .ToList();

                return Ok(medicines);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - SearchMedicines");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetStoreById(int StoreId)
        {
            try
            {
                StoreDTO store = new StoreDTO();
                store = (from s in _storeRepository.Find(s => s.StoreId == StoreId)
                         select new StoreDTO
                         {
                             StoreId = s.StoreId,
                             SupplerId = s.SupplerId,
                             MedicineId = s.MedicineId,
                             OriginalQty = s.OriginalQty,
                             RemainingQty = s.RemainingQty,
                             CostPrice = s.CostPrice,
                             TaxValue = s.TaxValue,
                             SellingPriceAfterTax = s.SellingPriceAfterTax,
                             SellingPriceBeforeTax = s.SellingPriceBeforeTax,
                             MaxDiscount = s.MaxDiscount,
                             ExpiaryDate = s.ExpiaryDate,
                             ProductionDate = s.ProductionDate,

                         }).FirstOrDefault();

                string JsonString = JsonConvert.SerializeObject(store, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - GetStoreById");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult UpdateStore(StoreDTO storeDTO)
        {
            try
            {
                Store store = new Store();
                store = _storeRepository.Find(s => s.StoreId == storeDTO.StoreId).FirstOrDefault();

                store.SupplerId = storeDTO.SupplerId;
                store.MedicineId = storeDTO.MedicineId;
                store.OriginalQty = storeDTO.OriginalQty;
                store.RemainingQty = storeDTO.OriginalQty;
                store.MaxDiscount = storeDTO.MaxDiscount;
                store.TaxValue = storeDTO.TaxValue;
                store.SellingPriceAfterTax = storeDTO.SellingPriceAfterTax;
                store.SellingPriceBeforeTax = storeDTO.SellingPriceBeforeTax;
                store.ExpiaryDate = storeDTO.ExpiaryDate;
                store.CostPrice = storeDTO.CostPrice;
                store.ProductionDate = storeDTO.ProductionDate;

                _storeRepository.Update(store);
                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - UpdateStore");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Delete(int StoreId)
        {
            try
            {
                Store store = new Store();
                store = _storeRepository.Find(s => s.StoreId == StoreId).FirstOrDefault();

                _storeRepository.Delete(store);
                return Ok();
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Store Controller - Delete");
                return BadRequest(ex.Message);
            }
        }

    }
}
