using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using RMDataManager.Models;
using RMDataManager.Library.DataAccess;

namespace RMDataManager.Controllers
{
    [Authorize(Roles = "Casheir")]
    public class ProductController : ApiController
    {
        // GET: Product
   
        [Route("api/product")]
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();
            return data.GetProducts();
        }
    }
}