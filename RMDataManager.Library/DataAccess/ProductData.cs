using System.Collections.Generic;
using RMDataManager.Models;
using RMDataManager.Library.Internal.DataAccess;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        private readonly IConfiguration _config;
        public ProductData(IConfiguration config)
        {
            _config = config;
        }
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMDatabaseConnection");
            return output;
        }
        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "RMDatabaseConnection").FirstOrDefault();
            return output;
        }
    }
}
