using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace RMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storeProcedure, U parametars, string connectionStringName)
        {
            string connection = GetConnectionString(connectionStringName);
            using (IDbConnection cnn = new SqlConnection(connection))
            {
                List<T> rows = cnn.Query<T>(storeProcedure, parametars, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        } 
        public void SaveData<T>(string storeProcedure, T parametars, string connectionStringName)
        {
            string connection = GetConnectionString(connectionStringName);
            using (IDbConnection cnn = new SqlConnection(connection))
            {
                 cnn.Execute(storeProcedure, parametars, commandType: CommandType.StoredProcedure);           
            }
        }



    }
}
