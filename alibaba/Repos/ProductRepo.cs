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

            var parameters = new DynamicParameters();
            parameters.Add("@id", prod.Id);
            parameters.Add("@name", prod.Name);
            parameters.Add("@restaurantId", prod.RestaurantId);
            parameters.Add("@category", prod.Category);
            parameters.Add("@subcategory", prod.SubCategory);
            parameters.Add("@price", prod.Price);
            parameters.Add("@ingredients", prod.Ingredients);
            parameters.Add("@isvegan", prod.IsVegan);
            parameters.Add("@isvegiterian", prod.IsVegiterian);
            parameters.Add("@islowfat", prod.IsLowFat);
            parameters.Add("@calories", prod.Calories);
            parameters.Add("@image", prod.Image);
            parameters.Add("@eta", prod.ETA);
            parameters.Add("@rating", prod.Rating);
            parameters.Add("@description", prod.Description);


            try
            {
                var res = await sqlQuery.PostQuery(@"insert into products (name,category,subcategory,description,price, restaurantId , ingredients,isvegan ,isvegiterian,callories,image,eta,rating) VALUES 
(@name,@category,@subcategory,@description,@price, @restaurantId ,@ingredients,@isvegan 
,@isvegiterian,@calories,@image,@eta,@rating) ",
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

                    return new DbResponse { 
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
                result = await sql.GetQuery(@"SELECT name, price, offer,offerexpirystring, restaurantId, category,subcategory, ingredients, isvegan, isvegiterian, islowfat, image,eta,rating, description, callories   FROM products where Id = @Id ", parameters);
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
            parameters.Add("@islowfat", prod.IsLowFat);
            parameters.Add("@ingredients", prod.Ingredients);
            parameters.Add("@isvegan", prod.IsVegan);
            parameters.Add("@isvegiterian", prod.IsVegiterian);
            parameters.Add("@id", prod.Id);

            try
            {
                var res = await sqlQuery.PostQuery($"UPDATE products SET name ='{prod.Name}',category= '{prod.Category}', subcategory= '{prod.SubCategory}', description ='{prod.Description}',price='{prod.Price}'," +
                    $"ingredients='{prod.Ingredients}',isvegan='{prod.IsVegan}',isvegiterian='{prod.IsVegiterian}'" +
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
        public async Task<IEnumerable<DbProduct>> FilterProducts(DbProductCriteria  criteria)
    {
        SqlORM<DbProduct> sql = new SqlORM<DbProduct>(_dbSettings);
        var parameters = new DynamicParameters();
        string top = "name";
            string query = "";
            string range = " ";
        if(criteria.RestaurantId!=0)
            {
                query += $" and restaurantId = {criteria.RestaurantId}";
            }
        if(criteria.Category!=0)
            {
                query += $" and category = {criteria.Category }";
            }
        if(criteria.SubCategory!=0)
            {
                query += $" and subcategory = {criteria.SubCategory }";
            }
        if(criteria.isOffer)
            {
                query += $" and offerexpiry > DATE_ADD(NOW(), INTERVAL 9 HOUR)";
            }
        if (criteria.IsTop)
        {
            top = " rating desc";

        }
        if (criteria.PageNumber!=0)
        {
            range += $" LIMIT {criteria.PageSize * (criteria.PageNumber - 1)},{criteria.PageNumber * criteria.PageSize}";

        }
        parameters.Add("@name", criteria.Name);


        try
        {
            var res =await sql.GetListQuery($@"SELECT id, name, category,subcategory,rating, image, price,description, offer, offerexpirystring from products
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
