using System.Collections.Generic;
using RMDataManager.Models;
using RMDataManager.Library.Internal.DataAccess;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll",new { }, "RMDatabaseConnection");
            return output;
        }
    }
}
