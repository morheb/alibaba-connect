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
    public class ProductRepo : IProductRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<ProductRepo> _logger;
        public ProductRepo(IDbSettings dbSettings, ILogger<ProductRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        //public Task<IEnumerable<DbProduct>> FilterProductsList(DbProductCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<bool> PostProductAsync(DbProduct prod)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);
            var productId = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@name", prod.Name);
            parameters.Add("@restaurantId", prod.RestaurantId);
            parameters.Add("@category", prod.Category);
            parameters.Add("@subcategory", prod.SubCategory);
            parameters.Add("@price", prod.Price);
            parameters.Add("@ingredients", prod.Ingredients);
            parameters.Add("@isvegan", prod.IsVegan);
            parameters.Add("@isvegiterian", prod.IsVegiterian);
            parameters.Add("@IsDiaryFree", prod.IsDiaryFree);
            parameters.Add("@IsOrganic", prod.IsOrganic);
            parameters.Add("@IsZeroSugar", prod.IsZeroSugar);
            parameters.Add("@callories", prod.Calories);
            parameters.Add("@image", prod.Image);
            parameters.Add("@eta", prod.ETA);
            parameters.Add("@rating", prod.Rating);
            parameters.Add("@unit", prod.Unit);
            parameters.Add("@description", prod.Description);
            try
            {
                productId = await sqlQuery.GetQuery(@"SELECT `AUTO_INCREMENT` FROM INFORMATION_SCHEMA.TABLES WHERE 
                                                TABLE_SCHEMA = 'qeiapmmy_AllBaba_store' AND
                                                TABLE_NAME = 'products'  ", parameters) - 1;

            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }

            try
            {
                var res = await sqlQuery.PostQuery(@"insert into products (name,unit,category,subcategory,IsZeroSugar,IsOrganic,IsDiaryFree,isvegiterian,isvegan,description,price,restaurantId ,ingredients,callories,image,eta,rating) VALUES 
            (@name,@unit,@category,@subcategory,@IsZeroSugar,@IsOrganic,@IsDiaryFree,@isvegiterian,@isvegan,@description,@price, @restaurantId ,@ingredients,@callories,@image,@eta,@rating) ",
               parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }


            return true;
        }

        //public Task<bool> SetProductStatusAsync(DbProductStatus dbProdStatus)
        //{
        //    throw new NotImplementedException();
        //}



        //public async Task<IEnumerable<DbProduct>> FilterProductsAsync(DbProductCriteria criteria)
        //{

        //    string searchQuery = "";
        //    SqlORM<DbProduct> sqlQuery = new SqlORM<DbProduct>(_dbSettings);
        //    var parameters = new DynamicParameters();
        //    if (!string.IsNullOrEmpty(criteria.Name))
        //    {
        //        parameters.Add("@name", criteria.Name);


        //        searchQuery = $"WHERE name='{criteria.Name}'";
        //    }
        //    return await sqlQuery.GetListQuery($@"SELECT  Id, location, logo, FROM Manufacturers {searchQuery} ", parameters);
        //}


        public async Task<DbResponse> DeleteByIdAsync(int resId)
        {
            SqlORM<int> sql = new SqlORM<int>(_dbSettings);
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", resId);

            try
            {
                result = await sql.PostQuery(@"delete from products where id = @Id ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {
                    var n = ex.Number;

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    return new DbResponse
                    {
                        Data = "error",
                        Error = ex.Message,
                        Success = false

                    };
                }
            }
            return new DbResponse
            {
                Data = "success",
                Error = null,
                Success = true

            };
        }

        

        public async Task<DbResponse> UpdatePrices(int resId, double percentage)
        {
            SqlORM<int> sql = new SqlORM<int>(_dbSettings);
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@Percentage", percentage);
            parameters.Add("@RestaurantId", resId);

            try
            {
                result = await sql.PostQuery(@" 
            UPDATE products
            SET price = price + (price * @Percentage)
            WHERE RestaurantId = @RestaurantId ", parameters);
            }
            catch (MySqlException ex)
            {

                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    return new DbResponse
                    {
                        Data = "error",
                        Error = ex.Message,
                        Success = false

                    };
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Sequence contains no elements"))
                {

                    return new DbResponse
                    {
                        Data = "error",
                        Error = ex.Message,
                        Success = false

                    };
                }
            }
            return new DbResponse
            {
                Data = "success",
                Error = null,
                Success = true

            };
        }


        public async Task<DbProduct> GetProductByIdAsync(int resId)
        {
            SqlORM<DbProduct> sql = new SqlORM<DbProduct>(_dbSettings);
            DbProduct result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", resId);

            try
            {
                result = await sql.GetQuery(@"SELECT*  FROM products where Id = @Id ", parameters);
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


        public async Task<bool> UpdateProductAsync(DbProduct prod)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


            var parameters = new DynamicParameters();
            parameters.Add("@name", prod.Name);
            parameters.Add("@calories", prod.Calories);
            parameters.Add("@description", prod.Description);
            parameters.Add("@eta", prod.ETA);
            parameters.Add("@rating", prod.Rating);
            parameters.Add("@image", prod.Image);
            parameters.Add("@category", prod.Category);
            parameters.Add("@isOrganic", prod.IsOrganic);
            parameters.Add("@ingredients", prod.Ingredients);
            parameters.Add("@isvegan", prod.IsVegan);
            parameters.Add("@isvegiterian", prod.IsVegiterian);
            parameters.Add("@id", prod.Id);
            parameters.Add("@unit", prod.Unit);

            try
            {
                var res = await sqlQuery.PostQuery($"UPDATE products SET unit = '{prod.Unit}',name ='{prod.Name}',category= '{prod.Category}', subcategory= '{prod.SubCategory}', description ='{prod.Description}',price='{prod.Price}'," +
                    $"ingredients='{prod.Ingredients}',brand = '{prod.Brand}',isZeroSugar={prod.IsZeroSugar},isOrganic={prod.IsOrganic},isDiaryFree={prod.IsDiaryFree},isvegan={prod.IsVegan},isvegiterian={prod.IsVegiterian}" +
                    $",callories='{prod.Calories}'," +
                    $"image='{prod.Image}',eta='{prod.ETA}' WHERE  id = '{prod.Id}';", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }
        public async Task<bool> SetOfferAsync(DbProductOfferRequest prod)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);
            var expireydate = prod.OfferExpiry.ToString("yyyy-MM-dd HH-mm-ss");

            var expstring = expireydate.ToString();
            var parameters = new DynamicParameters();

            parameters.Add("@id", prod.Offer);

            try
            {
                var res = await sqlQuery.PostQuery($"UPDATE products SET offer ='{prod.Offer}', offerexpirystring='{expstring}',	offerexpiry= '{expireydate}' WHERE  id = '{prod.ProductId}';", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }




            return true;
        }
        public async Task<IEnumerable<DbProduct>> GetProductListByIdsAsync(List<int> ids)

        {
            SqlORM<DbProduct> sql = new SqlORM<DbProduct>(_dbSettings);
            var parameters = new DynamicParameters();


            try
            {
                string query = "";
                foreach (int id in ids)
                {
                    query += $" {id},";
                }
               
                query = query.TrimEnd(',');
               

                var res = await sql.GetListQuery($@"SELECT * from products
                                                where id in ( {query} )

                                                ", parameters);

                return res;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        public async Task<IEnumerable<DbProduct>> FilterProducts(DbProductCriteria criteria)
        {
            SqlORM<DbProduct> sql = new SqlORM<DbProduct>(_dbSettings);
            var parameters = new DynamicParameters();
            string top = "name";
            string query = "";
            string range = " ";
            if (criteria.RestaurantId != 0)
            {
                query += $" and restaurantId = {criteria.RestaurantId}";
            }
            if (criteria.Category != 0)
            {
                query += $" and category = {criteria.Category }";
            }
            if (criteria.Brand != 0)
            {
                query += $" and brand = {criteria.Brand }";
            }
            if (criteria.SubCategory != 0)
            {
                query += $" and subcategory = {criteria.SubCategory }";
            }
            if (criteria.IsDiaryFree)
            {
                query += $" and IsDiaryFree=true ";
            }
            if (criteria.IsOrganic)
            {
                query += $" and IsOrganic=true ";
            }
            if (criteria.IsVegan)
            {
                query += $" and isVegan=true ";
            }
            if (criteria.IsVegiterian)
            {
                query += $" and IsVegiterian=true ";
            }
            if (criteria.IsZeroSugar)
            {
                query += $" and IsZeroSugar=true ";
            }
            if (criteria.isOffer)
            {
                query += $" and offerexpiry > DATE_ADD(NOW(), INTERVAL 9 HOUR)";
            }
            if (criteria.IsTop)
            {
                top = " rating desc";

            }

            if (criteria.PageNumber != 0)
            {
                range += $" LIMIT {criteria.PageSize * (criteria.PageNumber - 1)},{criteria.PageNumber * criteria.PageSize}";

            }
            parameters.Add("@name", criteria.Name);


            try
            {
                var res = await sql.GetListQuery($@"SELECT * from products
                                                where name like '%{criteria.Name}%' {query} and rating >= {criteria.Rating}   order by {top}  {range}

                                                ", parameters);

                return res;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }


        //public async Task<bool> SetProductStatusAsync(DbProductStatus prodaurant)
        //{
        //    SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


        //            var parameters = new DynamicParameters();
        //            parameters.Add("@id", prodaurant.Id);

        //            parameters.Add("@status", prodaurant.Status);


        //            try
        //            {
        //                var res = await sqlQuery.PostQuery(@"UPDATE prodaurant SET status =@status, 

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
