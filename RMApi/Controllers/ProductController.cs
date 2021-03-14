using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Cashier, Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _prodcutData;
        public ProductController(IProductData productData)
        {
            _prodcutData = productData;
        }


        [HttpGet]
        [Route("product")]
 
        public List<ProductModel> Get()
        {
            return _prodcutData.GetProducts();
        }
    }
}
