using System.Collections.Generic;
using RMDataManager.Models;
using RMDataManager.Library.Internal.DataAccess;
using System.Linq;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMDatabaseConnection");
            return output;
        }
        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "RMDatabaseConnection").FirstOrDefault();
            return output;
        }
    }
}
