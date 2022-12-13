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
    public class RestaurantRepo : IRestaurantRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<RestaurantRepo> _logger;
        public RestaurantRepo(IDbSettings dbSettings, ILogger<RestaurantRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<DbResponse> PostRestaurantAsync(DbRestaurant rest)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();


            parameters.Add("@ownerUid", rest.OwnerUid);
            parameters.Add("@name", rest.Name);
            parameters.Add("@location", rest.Location);
            parameters.Add("@locationDesription", rest.LocationDescription);
            parameters.Add("@logo", rest.Logo);
            parameters.Add("@rating", rest.Rating);
            parameters.Add("@workingHoursEnd", rest.WorkingHoursEnd);
            parameters.Add("@workingHoursstart", rest.WorkingHoursStart);
            parameters.Add("@phonenumber", rest.PhoneNumber);
            parameters.Add("@firebasetoken", rest.FirebaseToken);
            parameters.Add("@whatsapp", rest.Whatsapp);
            parameters.Add("@description", rest.Description);
            parameters.Add("@id", rest.Id);

            
            try
            {
                var res = await sqlQuery.PostQuery(@"insert into restaurants (name, ownerUid,whatsapp,description,phonenumber,
                                                                               workingHoursstart,workingHoursEnd,rating, firebasetoken,
                                                                               logo,location ,locationDescription) VALUES
                                                                               (@name,@ownerUid,@whatsapp,@description,@phonenumber,
                                                                               @workingHoursstart,@workingHoursEnd,@rating,@firebasetoken,
                                                                               @logo,@location ,@locationDesription) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }

            int restId = 0;
            try
            {
                restId = await sqlQuery.GetQuery(@"SELECT `AUTO_INCREMENT` FROM INFORMATION_SCHEMA.TABLES WHERE 
                                                TABLE_SCHEMA = 'qeiapmmy_talabak' AND
                                                TABLE_NAME = 'restaurants'  ", parameters) - 1;

            }
            catch (Exception e)
            {
                return new DbResponse()
                {
                    Data = restId.ToString(),
                    Error = e.Message,
                    Success = false
                };
            }
            return new DbResponse()
            {
                Data = restId.ToString(),
                Error = null,
                Success = true
            };

        }



       

            public async Task<DbRestaurant> GetRestaurantByIdAsync(int resId)
        {
            SqlORM<DbRestaurant> sql = new SqlORM<DbRestaurant>(_dbSettings);
             DbRestaurant result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", resId);

            try
            {
                result = await sql.GetQuery(@"SELECT * FROM restaurants where Id = @Id ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    result = null;
                }
            }
            return result;
        }
            public async Task<DbRestaurant> GetMyRestaurantByIdAsync(string resId)
        {
            SqlORM<DbRestaurant> sql = new SqlORM<DbRestaurant>(_dbSettings);
             DbRestaurant result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", resId);

            try
            {
                result = await sql.GetQuery(@"SELECT * FROM restaurants where ownerUId = @Id ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    result = null;
                }
            }
            return result;
        }


        public async Task<bool> UpdateRestaurantAsync(DbRestaurant rest)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            
                    var parameters = new DynamicParameters();
                    parameters.Add("@name", rest.Name);
                    parameters.Add("@location", rest.Location);
                    parameters.Add("@locationDesription", rest.LocationDescription);
                    parameters.Add("@logo", rest.Logo);
                    parameters.Add("@status", rest.Status);
                    parameters.Add("@rating", rest.Rating);
                    parameters.Add("@workingHoursEnd", rest.WorkingHoursEnd);
                    parameters.Add("@workingHoursstart", rest.WorkingHoursStart);
                    parameters.Add("@phonenumber", rest.PhoneNumber);
                    parameters.Add("@whatsapp", rest.Whatsapp);
                    parameters.Add("@firebasetoken", rest.FirebaseToken);
                    parameters.Add("@description", rest.Description);
                    parameters.Add("@id", rest.Id);
                    


                    try
                    {
                        var res = await sqlQuery.PostQuery(@"UPDATE restaurants SET name =@name, firebasetoken =@firebasetoken, 
                                                            location= @location, description =@description,
                                                            phonenumber=@phonenumber, logo=@logo,  isactive = true,
                                                             rating=@rating,workingHoursEnd=@workingHoursEnd,
                                                             workingHoursstart=@workingHoursstart,locationDescription=@locationDesription,
                                                            whatsapp=@whatsapp
                                                            WHERE  id = @id;", parameters);
                    }
                    catch (Exception e)
                    {
                        throw (new Exception(e.Message));
                    }
               

            

            return true;
        }
        public async Task<IEnumerable<DbRestaurant>> FilterRestaurantsList(DbRestaurantCriteria criteria)
        {
            SqlORM<DbRestaurant> sql = new SqlORM<DbRestaurant>(_dbSettings);
            var parameters = new DynamicParameters();
            string top = "name";
            if (criteria.IsTop)
            {
                top = "rating desc";
               
            };
            var query = "";
            if (criteria.IsActive)
            {
                parameters.Add("@name", criteria.Name);


                query += $"and isactive=1";
            }
            parameters.Add("@name", criteria.Name);


            try
            {
                return await sql.GetListQuery($@"SELECT id,isactive, name,LocationDescription, rating,phonenumber,description,firebasetoken, workinghoursstart, workinghoursend,status, logo from restaurants
                                                where name like '%{criteria.Name}%'  {query} and rating >= {criteria.Rating} order by {top} LIMIT {criteria.PageSize*(criteria.PageNumber-1)},{criteria.PageNumber*criteria.PageSize}
                                               
                                                ", parameters);
            }
            catch(Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        public async Task<bool> SetRestaurantStatusAsync(DbRestaurantStatus restaurant)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            
                    var parameters = new DynamicParameters();
                    parameters.Add("@id", restaurant.Id);
                    parameters.Add("@isactive", restaurant.IsActive);
                 
                    parameters.Add("@status", restaurant.Status);


                    try
                    {
                        var res = await sqlQuery.PostQuery($@"UPDATE restaurants SET status =@status , isactive = {restaurant.IsActive}
                                                        
                                                            WHERE  id = @id;", parameters);
                    }
                    catch (Exception e)
                    {
                        throw (new Exception(e.Message));
                    }
               
            return true;
        }

      
    }
}
