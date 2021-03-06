﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly IConfiguration _config;
        public SalesController(IConfiguration config)
        {
            _config = config;
        }
        [Authorize(Roles = "Casheir")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(_config);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            data.SaveSale(sale, userId);
        }
        [Authorize(Roles = "Admin, Manager")]
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData(_config);
            return data.GetSaleReport();
        }
    }
}