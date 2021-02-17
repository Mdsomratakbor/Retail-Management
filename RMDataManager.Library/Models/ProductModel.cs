﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMDataManager.Models
{
    public class ProductModel
    {
        /// <summary>
        /// The unique identifier for a given product.
        /// </summary>
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityStock { get; set; }
        public bool IsTaxable { get; set; }
    }
}