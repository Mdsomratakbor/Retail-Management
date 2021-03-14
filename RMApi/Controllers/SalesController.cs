using Microsoft.AspNetCore.Authorization;
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
        private readonly ISaleData _saleData;

        public SalesController(ISaleData saleData)
        {
            _saleData = saleData;
        }
        [Authorize(Roles = "Casheir")]
        [HttpPost]
        public void Post(SaleModel sale)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _saleData.SaveSale(sale, userId);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        [Route("GetSalesReport")]
    
        public List<SaleReportModel> GetSalesReport()
        {
            return _saleData.GetSaleReport();
        }
    }
}
