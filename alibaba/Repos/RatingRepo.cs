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
    public class RatingRepo : IRatingRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<RatingRepo> _logger;
        public RatingRepo(IDbSettings dbSettings, ILogger<RatingRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        //public Task<IEnumerable<DbAddon>> FilterAddonsList(DbAddonCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<bool> PostRatingAsync(DbRating rating)
        {
            SqlORM<double> sqlQuery = new SqlORM<double>(_dbSettings);

            var parameters = new DynamicParameters();
            parameters.Add("@productId", rating.ProductId);
            parameters.Add("@stars", rating.Stars);
            parameters.Add("@userId", rating.UserId);

            try
            {
                var existingRow = await sqlQuery.GetQuery(@"SELECT COUNT(*) FROM rating 
                                                    WHERE productId = @productId 
                                                    AND userId = @userId", parameters);

                if (existingRow > 0)
                {
                    await sqlQuery.PostQuery(@"UPDATE rating 
                                       SET stars = @stars 
                                       WHERE productId = @productId 
                                       AND userId = @userId",
                                               parameters);
                }
                else
                {
                    await sqlQuery.PostQuery(@"INSERT INTO rating (productId, stars, userId) 
                                       VALUES (@productId, @stars, @userId)",
                                               parameters);
                }

                var averageRatingParams = new DynamicParameters();
                averageRatingParams.Add("@productId", rating.ProductId);

                var averageRating = await sqlQuery.GetQuery(@"SELECT ROUND(AVG(stars), 2) 
                                                     FROM rating 
                                                     WHERE productId = @productId",
                                                             averageRatingParams);

                var updateProductParams = new DynamicParameters();
                updateProductParams.Add("@productId", rating.ProductId);
                updateProductParams.Add("@averageRating", averageRating);

                await sqlQuery.PostQuery(@"
    UPDATE products 
    SET rating = @averageRating 
    WHERE id = @productId", 
                    updateProductParams);





                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
