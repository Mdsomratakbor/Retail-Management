﻿using RMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class SalesController : ApiController
    {
        public void Post(SaleModel sale)
        {
            Console.WriteLine();
        }
    }
}
 