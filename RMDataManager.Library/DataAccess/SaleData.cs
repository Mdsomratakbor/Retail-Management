using RMDataManager.Library.Helper;
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
        public void SaveSale(SaleModel saleInfo,string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData product = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate()/100;
            foreach (var item in saleInfo.SalesDetails)
            {
               var  detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
              
               var productInfo = product.GetProductById(item.ProductId);
                if(productInfo== null)
                {
                    throw new Exception($"The Product Id of {item.ProductId} could not be found int the database.");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);
                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x=>x.Tax),
                CashierId = cashierId, 

            };
            sale.Total = sale.SubTotal + sale.Tax;

            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData<SaleDBModel>("dbo.spSale_Insert", sale, "RMDatabaseConnection");

            sale.Id = sql.LoadData<int, dynamic>("dbo.spSale_Lookup", new { CashierId = sale.CashierId, SaleDate = sale.SaleDate }, "RMDatabaseConnection").FirstOrDefault();
            details.ForEach(x => {
                x.SaleId = sale.Id; sql.SaveData("dbo.spSalesDetail_Insert", x, "RMDatabaseConnection");
            }); 


            //foreach (var item in details)
           // {
           //     item.SaleId = sale.Id;
           //     sql.SaveData//("dbo.spSalesDetail_Insert", item, "RMDatabaseConnection");
           // }


        }
    }
}
