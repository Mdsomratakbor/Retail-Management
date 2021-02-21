using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel sale)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData product = new ProductData();
            foreach (var item in sale.SalesDetails)
            {
               var  detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
              
               var productInfo = product.GetProductById(item.ProductId);
                details.Add(detail);
            }

            // TOD: Make this SOLID/DRY/Better
            // Start  filling ih the 
        }
    }
}
