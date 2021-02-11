using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using RMDataManager.Models;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        // GET: Product
        public List<ProductModel> Index()
        {
            return View();
        }
    }
}