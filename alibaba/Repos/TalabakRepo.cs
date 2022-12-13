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
using System.Linq;

namespace alibaba.Repos
{
    public class TalabakRepo : ITalabakRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<TalabakRepo> _logger;
        public TalabakRepo(IDbSettings dbSettings, ILogger<TalabakRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        //public Task<IEnumerable<DbTalabak>> FilterTalabaksList(DbTalabakCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<DbResponse> PostTalabakAsync(DbTalabak talabak)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();

            parameters.Add("@driverId", talabak.DriverId);

            try
            {
                var res = await sqlQuery.PostQuery($@"insert into talabak (senderphone,sendername,senderid,status,senderlocation,senderlocationinfo,object,note,
                        receivername,receiverphone,receiverlocation,receiverlocationinfo,drivername,driverphone,fees,driverid, time) values(
                        '{talabak.SenderPhone}','{talabak.SenderName}','{talabak.SenderId}','{talabak.Status}','{talabak.SenderLocation}','{talabak.SenderLocationInfo}','{talabak.Object}',    '{talabak.Note}',    
                        '{talabak.ReceiverName}','{talabak.ReceiverPhone}','{talabak.ReceiverLocation}','{talabak.ReceiverLocationInfo}',
                        '{talabak.DriverName}','{talabak.DriverPhone}','{talabak.Fees}',{talabak.DriverId},'{talabak.Time}')",
               parameters);
            }
            catch (Exception e)
            {
                return new DbResponse()
                {
                    Data = "",
                    Error = e.Message,
                    Success = false
                };
            }
            parameters = new DynamicParameters();
            int talabakId = 0;
            if (talabak != null)
            {
                try
                {
                    talabakId = await sqlQuery.GetQuery(@"SELECT `AUTO_INCREMENT` FROM INFORMATION_SCHEMA.TABLES WHERE 
                                                TABLE_SCHEMA = 'qeiapmmy_talabak' AND
                                                TABLE_NAME = 'talabak'  ", parameters) - 1;

                }
                catch (Exception e)
                {
                    return new DbResponse()
                    {
                        Data = talabakId.ToString(),
                        Error = e.Message,
                        Success = false
                    };
                }
            }
                return new DbResponse()
                {
                    Data = talabakId.ToString(),
                    Error = null,
                    Success = true
                };
            
        }
        public async Task<bool> UpdateTalabakStatusAsync(DbTalabakStatus dbtalabakStatus)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();
            parameters.Add("@id", dbtalabakStatus.Id);
            parameters.Add("@status", dbtalabakStatus.Status);
            parameters.Add("@driverId", dbtalabakStatus.DriverId);
            parameters.Add("@driverName", dbtalabakStatus.DriverName)   ;
            parameters.Add("@driverNumber", dbtalabakStatus.DriverNumber);
           

            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE talabak SET 
                                                               status=@status , driverId =@driverId, driverName = @driverName, driverNumber = @driverNumber
                                                            WHERE  id = @id;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }



        //public async Task<IEnumerable<DbTalabak>> FilterTalabaksAsync(DbTalabakCriteria criteria)
        //{

        //    string searchQuery = "";
        //    SqlORM<DbTalabak> sqlQuery = new SqlORM<DbTalabak>(_dbSettings);
        //    var parameters = new DynamicParameters();
        //    if (!string.IsNullOrEmpty(criteria.Name))
        //    {
        //        parameters.Add("@name", criteria.Name);


        //        searchQuery = $"WHERE name='{criteria.Name}'";
        //    }
        //    return await sqlQuery.GetListQuery($@"SELECT  Id, location, logo, FROM Manufacturers {searchQuery} ", parameters);
        //}


        public async Task<DbTalabak> GetTalabakByIdAsync(int talabakId)
        {
            SqlORM<DbTalabak> sql = new SqlORM<DbTalabak>(_dbSettings);
            DbTalabak result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", talabakId);

            try
            {
                result = await sql.GetQuery(@"SELECT * from talabak where id = @id", parameters);
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

        public async  Task<int> GetTalabaksStatistics(DbTalabakCriteria criteria)
        {
            SqlORM<int> sql = new SqlORM<int>(_dbSettings);
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", criteria.DriverId);
            string query = "id > 0";
            if(criteria.DriverId != 0)
            {
                query += $" and driverId = {criteria.DriverId}";
            }
            if(criteria.SenderId != 0)
            {
                query += $" and senderId = {criteria.SenderId}";
            }
            if (criteria.Status != 0)
            {
                query += $" and status <  {criteria.Status}";
            }

            try
            {
                result = await sql.GetQuery($" SELECT COUNT(id)  FROM talabak where {query}; ", parameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
          
            return result;
        }

    



        //    public async Task<bool> UpdateTalabakAsync(DbTalabak talabak)
        //    {
        //        SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


        //        var parameters = new DynamicParameters();
        //        parameters.Add("@name", talabak.Name);
        //        parameters.Add("@calories", talabak.Calories);
        //        parameters.Add("@description", talabak.Description);
        //        parameters.Add("@eta", talabak.ETA);
        //        parameters.Add("@rating", talabak.Rating);
        //        parameters.Add("@image", talabak.Image);
        //        parameters.Add("@type", talabak.Type);
        //        parameters.Add("@restaurantId", talabak.RestaurantId);
        //        parameters.Add("@islowfat", talabak.IsLowFat);
        //        parameters.Add("@ingredients", talabak.Ingredients);
        //        parameters.Add("@isvegan", talabak.IsVegan);
        //        parameters.Add("@isvegiterian", talabak.IsVegiterian);
        //        parameters.Add("@id", talabak.Id);

        //        try
        //        {
        //            var res = await sqlQuery.PostQuery($"UPDATE talabaks SET name ='{talabak.Name}',type= '{talabak.Type}', description ='{talabak.Description}',price='{talabak.Price}'," +
        //                $" restaurantId='{talabak.RestaurantId}',ingredients='{talabak.Ingredients}',isvegan='{talabak.IsVegan}',isvegiterian='{talabak.IsVegiterian}'" +
        //                $",callories='{talabak.Calories}'," +
        //                $"image='{talabak.Image}',eta='{talabak.ETA}' WHERE  id = '{talabak.Id}';", parameters);
        //        }
        //        catch (Exception e)
        //        {
        //            throw (new Exception(e.Message));
        //        }

        //       return true;
        //    }

        public async Task<IEnumerable<DbTalabak>> FilterTalabaks(DbTalabakCriteria criteria)
        {
            SqlORM<DbTalabak> sql = new SqlORM<DbTalabak>(_dbSettings);
            var parameters = new DynamicParameters();

            string query = "senderId !=0";
            if (criteria.Status!= 0)
            {
                query += $" and status <  {criteria.Status}";
            }
            if (criteria.SenderId != 0)
            {
                query  +=$" and senderId = {criteria.SenderId}";
            }

            if (criteria.DriverId != 0)
            {
                query += $" and driverId = {criteria.DriverId}";
            }
       


            IEnumerable<DbTalabak> talabaks = Enumerable.Empty<DbTalabak>();
            try
            {
                talabaks= await sql.GetListQuery($@"SELECT * from talabak  where {query} LIMIT {criteria.PageSize * (criteria.PageNumber - 1)},{criteria.PageNumber * criteria.PageSize}

                                                    ", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            return talabaks;



        }
        //public async Task<bool> SetTalabakStatusAsync(DbTalabakStatus talabakaurant)
        //{
        //    SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


        //            var parameters = new DynamicParameters();
        //            parameters.Add("@id", talabakaurant.Id);

        //            parameters.Add("@status", talabakaurant.Status);


        //            try
        //            {
        //                var res = await sqlQuery.PostQuery(@"UPDATE talabakaurant SET status =@status, 

        //                                                    WHERE  id = @id;", parameters);
        //            }
        //            catch (Exception e)
        //            {
        //                throw (new Exception(e.Message));
        //            }

        //    return true;
        //}
    }

}

