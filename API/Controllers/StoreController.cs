﻿using API.Services;
using EntitiyComponent.DBEntities;
using Infrastructure.DTO;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
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

                return View();
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
                              CostPrice = obj.CostPrice,
                              OriginalQty = obj.OriginalQty,
                              MedicineDescription = obj.Medicine.Description,
                              DepartmentName = obj.Medicine.MedicineDepartment.DepartmentName,
                              MedicineName = obj.Medicine.MedicineName,

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

    }
}
