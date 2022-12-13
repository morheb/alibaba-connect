namespace alibaba.Common
{
  
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using Dapper;
    using System.Data;
    using System;
    using alibaba.Common;
    using MySql.Data.MySqlClient;
    using Microsoft.Extensions.Logging;

    public class SqlORM<T>
    {
        private readonly IDbSettings _dbSettings;

        public SqlORM(IDbSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        public async Task ExecuteProcedureLoop(string procedure, IEnumerable<DynamicParameters> parameters)
        {
            using (var dbConnection = _dbSettings.Connection())
            {
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        foreach (var parameter in parameters)
                        {
                            await dbConnection.ExecuteAsync(procedure, parameter, commandType: CommandType.StoredProcedure);
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                    }

                    transaction.Commit();
                }
            }
        }

        public async Task<int> ExecuteProcedure(string procedure, DynamicParameters parameters)
        {
            using (var dbConnection = _dbSettings.Connection())
            {
                return await dbConnection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //public async Task<IDataReader> ExecuteReaderProcedure(string procedure, DynamicParameters parameters)
        //{
        //    using (var dbConnection = _dbSettings.Connection())
        //    {
        //        var result = await dbConnection.ExecuteReaderAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //}

        public async Task<int> PostQuery(string query, DynamicParameters parameters)
        {
            using (var dbConnection = _dbSettings.Connection())
            {
                return await dbConnection.ExecuteScalarAsync<int>(query, parameters);
            }
        }

        public async Task<T> GetQuery(string query, DynamicParameters parameters)
        {
            T result = default(T);

            using (var dbConnection = _dbSettings.Connection())
            {
                try
                {
                   
                    dbConnection.Open();

                    
                    result = await dbConnection.QueryFirstAsync<T>(query, parameters);
                    return result;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                   
                }
            }

            return result;
        }


        public async Task<IEnumerable<T>> GetListQuery(string query, DynamicParameters parameters)
        {
            using (var dbConnection = _dbSettings.Connection())
            {
                try
                {
                    var result = await dbConnection.QueryAsync<T>(query, parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                return null;
            }
        }



    }
}


