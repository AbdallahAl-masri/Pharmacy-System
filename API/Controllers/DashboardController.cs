﻿using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.IRepository;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IErrorLogService _errorLogService;

        public DashboardController(IStoreRepository storeRepository, ISupplierRepository supplierRepository,
            IErrorLogService errorLogService)
        {
            _storeRepository = storeRepository;
            _supplierRepository = supplierRepository;
            _errorLogService = errorLogService;
        }

        [HttpGet]
        public IActionResult GetDashboardDetails()
        {
            try
            {
                var expiryDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30));
                var today = DateOnly.FromDateTime(DateTime.Today);

                DashboardDTO dashboardDTO = new DashboardDTO();
                dashboardDTO.MedicineCount = _storeRepository.Find(x =>
                    x.ExpiaryDate < expiryDate && x.ExpiaryDate >= today).Count();

                dashboardDTO.SupplierCount = _supplierRepository.GetAll().Count();
                string JsonString = JsonConvert.SerializeObject(dashboardDTO, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(JsonString);
            }
            catch (Exception ex)
            {
                _errorLogService.AddErrorLog(ex, "Dashboard Controller - GetDashboardDetails");
                return BadRequest(ex.Message);
            }
        }
    }
}
