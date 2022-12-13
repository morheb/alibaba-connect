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
    public class CategoryRepo : ICategoryRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<CategoryRepo> _logger;
        public CategoryRepo(IDbSettings dbSettings, ILogger<CategoryRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<bool> PostCategoryAsync(DbCategory cat)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();


            parameters.Add("@name", cat.Name);
            parameters.Add("@restaurantId", cat.RestaurantId);
            parameters.Add("@id", cat.Id);


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into restaurantcategories (name, restaurantId) VALUES
                                                                               (@name,@restaurantId) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }






        public async Task<bool> UpdateCategoryAsync(DbCategory cat)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            
                    var parameters = new DynamicParameters();
                    parameters.Add("@name", cat.Name);
                    parameters.Add("@restaurantId", cat.RestaurantId);
                
                    parameters.Add("@id", cat.Id);
                    


                    try
                    {
                        var res = await sqlQuery.PostQuery(@"UPDATE restaurantcategories SET name =@name, 
                                                            
                                                            restaurantId=@restaurantId
                                                            WHERE  id = @id;", parameters);
                    }
                    catch (Exception e)
                    {
                        throw (new Exception(e.Message));
                    }
               

            

            return true;
        }
        public async Task<IEnumerable<DbCategory>> FilterCategories(int restId)
        {
            SqlORM<DbCategory> sql = new SqlORM<DbCategory>(_dbSettings);
            var parameters = new DynamicParameters();
           
            try
            {
                return await sql.GetListQuery($@"SELECT id, name,restaurantId from restaurantcategories
                                                where restaurantId = {restId}
                                                ", parameters);
            }
            catch(Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        public async Task<string> DeleteCategory(int catId)
        {
            SqlORM<string> sql = new SqlORM<string>(_dbSettings);
            var result = "";

            var parameters = new DynamicParameters();
            parameters.Add("@catId", catId);

            try
            {
                await sql.PostQuery(@"delete FROM restaurantcategories where Id = @catId ", parameters);
            }

            catch (Exception ex)
            {
                return (ex.Message);
            }
            return "success";
        }

    }
}
