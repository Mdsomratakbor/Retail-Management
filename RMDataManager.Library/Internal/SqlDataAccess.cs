using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace RMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess : IDisposable
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public string GetConnectionString(string name)
        {
            //return @"Server=DESKTOP-NAVD66F\SA;Database=RMData;Trusted_Connection=True";
            //  return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return _config.GetConnectionString(name);
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

        private IDbConnection _connection;
        private IDbTransaction _transation;

        public void SaveDataInTransaction<T>(string storeProcedure, T parametars)
        { 

            
                _connection.Execute(storeProcedure, parametars, commandType: CommandType.StoredProcedure, transaction: _transation);
            
        }

        public List<T> LoadDataInTransaction<T, U>(string storeProcedure, U parametars)
        {

           
                List<T> rows = _connection.Query<T>(storeProcedure, parametars, commandType: CommandType.StoredProcedure, transaction: _transation).ToList();
                return rows;
            
        }

        public void StartTransaction( string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transation = _connection.BeginTransaction();
            isClosed = false;
        }
        private bool isClosed = false;
      

        public void CommitTransaction()
        {
            _transation?.Commit();
            _connection?.Close();
            isClosed = true;
        }
        public void RollbackTransaction()
        {
            _transation.Rollback();
            _connection?.Close();
            isClosed = true;
        }

        public void Dispose()
        {
            if (isClosed == false) 
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception)
                {

                  //TODO: LOG THIS ISSUE
                }
            }
            _transation = null;
            _connection = null;
           
        }
        // Open connect/start transaction method
        // load using the transaction
        // save using the transaction
        // close connection/stop transaction method
        // Dispose

    }
}
