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
    public class BannerRepo : IBannerRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<BannerRepo> _logger;
        public BannerRepo(IDbSettings dbSettings, ILogger<BannerRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        public async Task<bool> PostBannerAsync(string url, int id)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();


         
            parameters.Add("@url", url);
            parameters.Add("@id", id);
            parameters.Add("@restaurantId", id);


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into banners (url,restaurantId) VALUES
                                                                               (@url,@restaurantId) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }


        public async Task<IEnumerable<DbBanner>> GetAllBanners( )
        {
            SqlORM<DbBanner> sql = new SqlORM<DbBanner>(_dbSettings);
            var parameters = new DynamicParameters();
            string query = " ";
           
            try
            {
                return await sql.GetListQuery($@"SELECT * from banners ", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<string> DeleteBannerAsync(int Id)
        {
            SqlORM<string> sql = new SqlORM<string>(_dbSettings);
            var result = "";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id);

            try
            {
                await sql.PostQuery(@"delete FROM banners where Id = @Id ", parameters);
            }

            catch (Exception ex)
            {
                return (ex.Message);
            }
            return "success";
        }


        public async Task<bool> UpdateBannerAsync(string url, int id)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
    
            parameters.Add("@url", url);
            parameters.Add("@id", id);



            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE banners SET url =@url WHERE  id = @id;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }

        public async Task<string> GetBanner(int id)
        {

            SqlORM<string> sql = new SqlORM<string>(_dbSettings);
            string result = "";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
           

            try
            {
                result = await sql.GetQuery(@"SELECT url FROM banners where id = @Id ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;
                    result = "";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    result = "";
                }
            }
            return result;
        }
    }
}
