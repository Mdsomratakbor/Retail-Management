using System.Collections.Generic;
using RMDataManager.Models;
using RMDataManager.Library.Internal.DataAccess;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sql;

        public ProductData( ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public List<ProductModel> GetProducts()
        {
          
            var output = _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMDatabaseConnection");
            return output;
        }
        public ProductModel GetProductById(int productId)
        {
    
            var output = _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "RMDatabaseConnection").FirstOrDefault();
            return output;
        }
    }
}
