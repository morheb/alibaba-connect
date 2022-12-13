using Dapper;
using MySql.Data.MySqlClient;
using alibaba.Common;
using alibaba.Data;
using alibaba.interfaces;
using alibaba.Common;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using alibaba.Services.Models;
using System.Collections.Generic;

namespace alibaba.Repos
{
    public class ConstantRepo : IConstantRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<ConstantRepo> _logger;
        public ConstantRepo(IDbSettings dbSettings, ILogger<ConstantRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<bool> PostCostantAsync(double value, int id, string name)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();


         
            parameters.Add("@value", value);
            parameters.Add("@name", name);
            parameters.Add("@id", id);


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into constants (value, name) VALUES
                                                                               (@value, @name) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }



         

        public async Task<bool> UpdateConstantAsync(double value, int id)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
    
            parameters.Add("@value", value);
            parameters.Add("@id", id);



            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE constants SET value =@value WHERE  id = @id;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }

        public async Task<double> GetConstant(int id)
        {

            SqlORM<double> sql = new SqlORM<double>(_dbSettings);
            double result = 0.0;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
           

            try
            {
                result = await sql.GetQuery(@"SELECT value FROM constants where id = @Id ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = 0.0;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    result = 0.0;
                }
            }
            return result;
        }
    }
}
