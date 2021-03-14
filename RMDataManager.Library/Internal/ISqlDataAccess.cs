using System.Collections.Generic;

namespace RMDataManager.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storeProcedure, U parametars, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storeProcedure, U parametars);
        void RollbackTransaction();
        void SaveData<T>(string storeProcedure, T parametars, string connectionStringName);
        void SaveDataInTransaction<T>(string storeProcedure, T parametars);
        void StartTransaction(string connectionStringName);
    }
}