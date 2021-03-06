using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        public SaleData(IConfiguration config)
        {
            _config = config;
        }
        public void SaveSale(SaleModel saleInfo,string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData product = new ProductData(_config);
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

            

            using (SqlDataAccess sql = new SqlDataAccess(_config))
            {
                try
                {
                    sql.StartTransaction("RMDatabaseConnection");
                    sql.SaveDataInTransaction<SaleDBModel>("dbo.spSale_Insert", sale);

                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("dbo.spSale_Lookup", new { CashierId = sale.CashierId, SaleDate = sale.SaleDate }).FirstOrDefault();

                    details.ForEach(item =>
                    {
                        item.SaleId = sale.Id; sql.SaveDataInTransaction("dbo.spSalesDetail_Insert", item);
                    });
                    sql.CommitTransaction();
                }
                catch 
                {

                    sql.RollbackTransaction();
                    throw;
                }

            }
        }

        public List<SaleReportModel> GetSaleReport()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var output = sql.LoadData<SaleReportModel, dynamic>("dbo.spSale_SaleReport", new { }, "RMDatabaseConnection");
            return output;
        }
    }
}
