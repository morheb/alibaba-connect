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
    public class AddonRepo : IAddonRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<AddonRepo> _logger;
        public AddonRepo(IDbSettings dbSettings, ILogger<AddonRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        //public Task<IEnumerable<DbAddon>> FilterAddonsList(DbAddonCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}

      
        public async Task<bool> PostAddonAsync(DbAddon addon)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();
            parameters.Add("@id", addon.Id);
            parameters.Add("@name", addon.Name);
            parameters.Add("@price", addon.Price);
            parameters.Add("@image", addon.Image);
            parameters.Add("@productId", addon.ProductId);
           


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into addons (name,productId,price, image) VALUES 
(@name,@productId,@price , @image) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }

        //public Task<bool> SetAddonStatusAsync(DbAddonStatus dbProdStatus)
        //{
        //    throw new NotImplementedException();
        //}

    

        //public async Task<IEnumerable<DbAddon>> FilterAddonsAsync(DbAddonCriteria criteria)
        //{

        //    string searchQuery = "";
        //    SqlORM<DbAddon> sqlQuery = new SqlORM<DbAddon>(_dbSettings);
        //    var parameters = new DynamicParameters();
        //    if (!string.IsNullOrEmpty(criteria.Name))
        //    {
        //        parameters.Add("@name", criteria.Name);


        //        searchQuery = $"WHERE name='{criteria.Name}'";
        //    }
        //    return await sqlQuery.GetListQuery($@"SELECT  Id, location, logo, FROM Manufacturers {searchQuery} ", parameters);
        //}


        public async Task<DbAddon> GetAddonByIdAsync(int resId)
        {
            SqlORM<DbAddon> sql = new SqlORM<DbAddon>(_dbSettings);
            DbAddon result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", resId);

            try
            {
                result = await sql.GetQuery(@"SELECT * FROM addons where Id = @Id ", parameters);
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


        public async Task<IEnumerable<DbAddon>> FilterAddons(DbAddonCriteria  criteria)
    {
        SqlORM<DbAddon> sql = new SqlORM<DbAddon>(_dbSettings);
        var parameters = new DynamicParameters();
        string top = "name";
            string query = "";
            string range = " ";
        if(criteria.ProductId!=0)
            {
                query = $" and productId = {criteria.ProductId}";
            }
        if(!String.IsNullOrEmpty(criteria.Name))
            {
                query = $" and name = {criteria.Name }";
            }
       
        parameters.Add("@name", criteria.Name);


        try
        {
            var res =await sql.GetListQuery($@"SELECT id, name, image , productId, price from addons
                                                where name like '%{criteria.Name}%' {query}   order by name ", parameters);

                return res;
        }
        catch (Exception e)
        {
            throw (new Exception(e.Message));
        }
    }

        public async Task<bool> UpdateAddonsAsync(DbAddon addon)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
            parameters.Add("@name", addon.Name);
            parameters.Add("@price", addon.Price);           
            parameters.Add("@id", addon.Id);

            try
            {
                var res = await sqlQuery.PostQuery($"UPDATE addons SET name ='{addon.Name}', price = {addon.Price}  , image = {addon.Image } WHERE  id = '{addon.Id}';", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }

        public  async Task<string> DeleteAddonsAsync(int id)
        {
            
                SqlORM<string> sql = new SqlORM<string>(_dbSettings);
                var result = "";

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                try
                {
                    await sql.PostQuery(@"delete FROM addons where Id = @Id ", parameters);
                }

                catch (Exception ex)
                {
                    return (ex.Message);
                }
                return "success";
            


        }
        //public async Task<bool> SetAddonStatusAsync(DbAddonStatus addonaurant)
        //{
        //    SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


        //            var parameters = new DynamicParameters();
        //            parameters.Add("@id", addonaurant.Id);

        //            parameters.Add("@status", addonaurant.Status);


        //            try
        //            {
        //                var res = await sqlQuery.PostQuery(@"UPDATE addonaurant SET status =@status, 

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
