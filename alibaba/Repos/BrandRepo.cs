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
    public class BrandRepo : IBrandRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<BrandRepo> _logger;
        public BrandRepo(IDbSettings dbSettings, ILogger<BrandRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<bool> PostBrandAsync(DbBrand cat)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();


            parameters.Add("@name", cat.Name);
            parameters.Add("@restaurantId", cat.RestaurantId);
            parameters.Add("@imgUrl", cat.ImgUrl);
            parameters.Add("@id", cat.Id);


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into restaurantbrands (name,imgUrl, restaurantId) VALUES
                                                                               (@name,@imgUrl,@restaurantId) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }






        public async Task<bool> UpdateBrandAsync(DbBrand cat)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            
                    var parameters = new DynamicParameters();
                    parameters.Add("@name", cat.Name);
                    parameters.Add("@restaurantId", cat.RestaurantId);
                    parameters.Add("@imgUrl", cat.ImgUrl);
                
                    parameters.Add("@id", cat.Id);
                    


                    try
                    {
                        var res = await sqlQuery.PostQuery(@"UPDATE restaurantbrands SET name =@name, 
                                                            imgUrl=@imgUrl,
                                                            restaurantId=@restaurantId
                                                            WHERE  id = @id;", parameters);
                    }
                    catch (Exception e)
                    {
                        throw (new Exception(e.Message));
                    }
               

            

            return true;
        }
        public async Task<IEnumerable<DbBrand>> FilterCategories(int restId)
        {
            SqlORM<DbBrand> sql = new SqlORM<DbBrand>(_dbSettings);
            var parameters = new DynamicParameters();
           
            try
            {
                return await sql.GetListQuery($@"SELECT id, name,restaurantId ,imgUrl from restaurantbrands
                                                where restaurantId = {restId}
                                                ", parameters);
            }
            catch(Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        public async Task<string> DeleteBrand(int catId)
        {
            SqlORM<string> sql = new SqlORM<string>(_dbSettings);
            var result = "";

            var parameters = new DynamicParameters();
            parameters.Add("@catId", catId);

            try
            {
                await sql.PostQuery(@"delete FROM restaurantbrands where Id = @catId ", parameters);
            }

            catch (Exception ex)
            {
                return (ex.Message);
            }
            return "success";
        }

    }
}
