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
    public class OrderRepo : IOrderRepo
    {

        private readonly IDbSettings _dbSettings;
        private readonly ILogger<OrderRepo> _logger;
        public OrderRepo(IDbSettings dbSettings, ILogger<OrderRepo> logger)
        {
            _dbSettings = dbSettings;
            _logger = logger;
        }

        //public Task<IEnumerable<DbOrder>> FilterOrdersList(DbOrderCriteria criteria)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<DbResponse> PostOrderAsync(DbOrder order)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();

            parameters.Add("@driverId", order.DriverId);
            parameters.Add("@userId", order.UserId);
            parameters.Add("@restaurantId", order.RestaurantId);
            parameters.Add("@extraFees", order.ExtraFees);
            parameters.Add("@withDelivery", order.WithDelivery);
            parameters.Add("@deliveryFees", order.DeliveryFees);
            parameters.Add("@type", order.Type);
            parameters.Add("@location", order.Location);
            parameters.Add("@time", order.Time);

            parameters.Add("@status", order.Status);
            parameters.Add("@price", order.Price);

            try
            {
                var res = await sqlQuery.PostQuery(@"insert into orders (restaurantId,time,location,driverId,status,price, userId, extraFees, withDelivery, deliveryFees, type) VALUES
                                                                         (@restaurantId,@time, @location,@driverId,@status,@price, @userId, @extraFees, @withDelivery, @deliveryFees,@type) ",
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
            int orderId = 0;
            if (order.Products != null && order.Products.Count > 0)
            {
                try
                {
                    orderId = await sqlQuery.GetQuery(@"SELECT `AUTO_INCREMENT` FROM INFORMATION_SCHEMA.TABLES WHERE 
                                                TABLE_SCHEMA = 'qeiapmmy_AllBaba_store' AND
                                                TABLE_NAME = 'orders'  ", parameters) - 1;

                }
                catch (Exception e)
                {
                    return new DbResponse()
                    {
                        Data = orderId.ToString(),
                        Error = e.Message,
                        Success = false
                    };
                }
                var query = "";
                foreach (DbProductOrder p in order.Products)
                {
                    query += $"({p.Id}, {p.Count}, {orderId}, {p.UnitPrice}, {p.TotalPrice}),";
                }
                if (query.EndsWith(','))
                {
                    query = query.Remove(query.Length - 1);
                }
                parameters = new DynamicParameters();

                try
                {
                    var res = await sqlQuery.PostQuery($@"insert into ordersproducts (productId,count,orderId, unitPrice, totalPrice) VALUES
                                                                              {query} ",
                   parameters);
                }
                catch (Exception e)
                {
                    var res = await sqlQuery.PostQuery($@"delete from orders where id = {orderId} ", parameters);
                    throw (new Exception(e.Message));
                }
                var addonQuery = "";
                if (order.Addons != null && order.Addons.Count > 0)
                {
                    foreach (DbAddonOrder a in order.Addons)
                    {
                        addonQuery += $"({orderId}, {a.Count}, {a.AddonId}, {a.Price}, '{a.Name}'),";
                    }
                    if (addonQuery.EndsWith(','))
                    {
                        addonQuery = addonQuery.Remove(addonQuery.Length - 1);
                    }
                    parameters = new DynamicParameters();

                   
                }

            }

            return new DbResponse()
            {
                Data = orderId.ToString(),
                Error = null,
                Success = true
            };
        }

        public async Task<bool> UpdateOrderStatusAsync(DbOrderStatus dborderStatus)
        {
            SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);

            var parameters = new DynamicParameters();
            parameters.Add("@id", dborderStatus.Id);
            parameters.Add("@status", dborderStatus.Status);
            parameters.Add("@driverId", dborderStatus.DriverId);
            parameters.Add("@withDelivery", dborderStatus.WithDelivery);

            try
            {
                var res = await sqlQuery.PostQuery(@"UPDATE orders SET 
                                                               status=@status , driverId=@driverId, withDelivery=@withDelivery
                                                            WHERE  id = @id;", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            return true;
        }



        //public async Task<IEnumerable<DbOrder>> FilterOrdersAsync(DbOrderCriteria criteria)
        //{

        //    string searchQuery = "";
        //    SqlORM<DbOrder> sqlQuery = new SqlORM<DbOrder>(_dbSettings);
        //    var parameters = new DynamicParameters();
        //    if (!string.IsNullOrEmpty(criteria.Name))
        //    {
        //        parameters.Add("@name", criteria.Name);


        //        searchQuery = $"WHERE name='{criteria.Name}'";
        //    }
        //    return await sqlQuery.GetListQuery($@"SELECT  Id, location, logo, FROM Manufacturers {searchQuery} ", parameters);
        //}


        public async Task<DbOrder> GetOrderByIdAsync(int orderId)
        {
            SqlORM<DbOrder> sql = new SqlORM<DbOrder>(_dbSettings);
            DbOrder result = null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", orderId);

            try
            {
                result = await sql.GetQuery(@"SELECT o.restaurantId,o.withDelivery, r.location as restLocation, u.phonenumber as phonenumber,o.time as time, o.driverId,o.status,o.price, o.userId, d.username as drivername, r.name as restaurantName , o.location as location ,o.extraFees ,u.username as username, o.deliveryFees, o.type FROM orders o join users u on o.userid = u.id join users d on d.id = o.driverId join restaurants r on r.id =o.restaurantId where o.Id = @Id ", parameters);
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
                return null;
            }
            SqlORM<DbProductOrder> prods = new SqlORM<DbProductOrder>(_dbSettings);
            parameters.Add("@Id", orderId);
            var products = await prods.GetListQuery(@"select productId as id, o.COUNT as count, p.name ,o.unitPrice,o.totalprice, p.image from ordersproducts o join products p on p.id = o.productId  where orderId = @Id", parameters);
            result.Products = products.AsList<DbProductOrder>();

            //SqlORM<DbAddonOrder> addonsSql = new SqlORM<DbAddonOrder>(_dbSettings);
            //parameters.Add("@Id", orderId);
            //var addons = await addonsSql.GetListQuery(@"select addonId , count, name  from orderAddon  where orderId = @Id", parameters);
            //result.Addons = addons.AsList<DbAddonOrder>();

            return result;
        }

        public async Task<int> GetOrdersStatistics(DbOrderCriteria criteria)
        {
            SqlORM<int> sql = new SqlORM<int>(_dbSettings);
            int result = 0;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", criteria.Id);
            string query = " id > 0 ";
            if (criteria.Status != 0)
            {
                if (criteria.Status != -1)
                {

                    query += $" and status <  {criteria.Status} and status !=-1 ";
                }
                else
                {
                    query += $"  and status =-1 ";

                }
            }
            if (criteria.Status == 0)
            {


                query += $"  and status !=-1 ";


            }
            if (criteria.DriverId != 0)
            {
                query += $" and driverId = {criteria.DriverId} ";
            }
            if (criteria.RestaurantId != 0)
            {
                query += $" and restaurantId = {criteria.RestaurantId} ";
            }

            if (criteria.UserId != 0)
            {
                query += $" and userId = {criteria.UserId} ";
            }
            if (criteria.Type != 0)
            {
                query += $" and type = {criteria.Type} ";
            }
            try
            {
                result = await sql.GetQuery($" SELECT COUNT(id)  FROM orders where {query}; ", parameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }





        //    public async Task<bool> UpdateOrderAsync(DbOrder order)
        //    {
        //        SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


        //        var parameters = new DynamicParameters();
        //        parameters.Add("@name", order.Name);
        //        parameters.Add("@calories", order.Calories);
        //        parameters.Add("@description", order.Description);
        //        parameters.Add("@eta", order.ETA);
        //        parameters.Add("@rating", order.Rating);
        //        parameters.Add("@image", order.Image);
        //        parameters.Add("@type", order.Type);
        //        parameters.Add("@restaurantId", order.RestaurantId);
        //        parameters.Add("@islowfat", order.IsLowFat);
        //        parameters.Add("@ingredients", order.Ingredients);
        //        parameters.Add("@isvegan", order.IsVegan);
        //        parameters.Add("@isvegiterian", order.IsVegiterian);
        //        parameters.Add("@id", order.Id);

        //        try
        //        {
        //            var res = await sqlQuery.PostQuery($"UPDATE orders SET name ='{order.Name}',type= '{order.Type}', description ='{order.Description}',price='{order.Price}'," +
        //                $" restaurantId='{order.RestaurantId}',ingredients='{order.Ingredients}',isvegan='{order.IsVegan}',isvegiterian='{order.IsVegiterian}'" +
        //                $",callories='{order.Calories}'," +
        //                $"image='{order.Image}',eta='{order.ETA}' WHERE  id = '{order.Id}';", parameters);
        //        }
        //        catch (Exception e)
        //        {
        //            throw (new Exception(e.Message));
        //        }

        //       return true;
        //    }

        public async Task<IEnumerable<DbOrder>> FilterOrders(DbOrderCriteria criteria)
        {
            SqlORM<DbOrder> sql = new SqlORM<DbOrder>(_dbSettings);
            var parameters = new DynamicParameters();

            string query = "userId !=0";
        
                query += $" and withDelivery = {criteria.WithDelivery} ";
            
            if (criteria.RestaurantId != 0)
            {
                query += $" and restaurantId = {criteria.RestaurantId}";
            }
            if (criteria.UserId != 0)
            {
                query += $" and userId = {criteria.UserId}";
            }
            if (criteria.Status != 0)
            {
                if (criteria.Status != -1)
                {

                    query += $" and o.status <  {criteria.Status} and o.status !=-1 ";
                }
                else
                {
                    query += $"  and o.status =-1";

                }
            }
            if (criteria.Status == 0)
            {


                query += $"  and o.status !=-1";


            }

            if (criteria.DriverId != 0)
            {
                query += $" and driverId = {criteria.DriverId}";
            }

            if (criteria.Type != 0)
            {
                query += $" and type = {criteria.Type}";
            }


            IEnumerable<DbOrder> orders = Enumerable.Empty<DbOrder>();
            try
            {
                orders = await sql.GetListQuery($@"SELECT o.id, o.withDelivery, r.location as restLocation,o.restaurantId,u.phonenumber as phonenumber, o.driverId,o.status,o.price, o.userId, d.username as drivername, r.name as restaurantName , o.location as location ,o.extraFees ,u.username as username, o.deliveryFees, o.type FROM orders o join users u on o.userid = u.id join users d on d.id = o.driverId join restaurants r on r.id =o.restaurantId
                                                    where {query} LIMIT {criteria.PageSize * (criteria.PageNumber - 1)},{criteria.PageNumber * criteria.PageSize}

                                                    ", parameters);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            var productSql = new SqlORM<DbProductOrder>(_dbSettings);
            parameters = new DynamicParameters();
            if(orders==null && orders.Count() == 0)
            {
                return Enumerable.Empty<DbOrder>();

            }
            foreach (DbOrder order in orders)
            {
                IEnumerable<DbProductOrder> products = Enumerable.Empty<DbProductOrder>();
                try
                {
                    products = await productSql.GetListQuery($@"SELECT* from ordersproducts join products on productId = products.id
                                                    where orderId = {order.Id}

                                                    ", parameters);

                    order.Products = products.AsList<DbProductOrder>();
                }

                catch (Exception e)
                {
                    throw (new Exception(e.Message));
                }
            }
            var addonSql = new SqlORM<DbAddonOrder>(_dbSettings);
            parameters = new DynamicParameters();
            foreach (DbOrder order in orders)
            {
                IEnumerable<DbAddonOrder> addons = Enumerable.Empty<DbAddonOrder>();
                try
                {
                    addons = await addonSql.GetListQuery($@"SELECT* from orderAddon
                                                    where orderId = {order.Id}

                                                    ", parameters);

                    order.Addons = addons.AsList<DbAddonOrder>();
                }

                catch (Exception e)
                {
                    throw (new Exception(e.Message));
                }

            }



            return orders;



        }
        //public async Task<bool> SetOrderStatusAsync(DbOrderStatus orderaurant)
        //{
        //    SqlORM<int> sqlQuery = new SqlORM<int>(_dbSettings);


        //            var parameters = new DynamicParameters();
        //            parameters.Add("@id", orderaurant.Id);

        //            parameters.Add("@status", orderaurant.Status);


        //            try
        //            {
        //                var res = await sqlQuery.PostQuery(@"UPDATE orderaurant SET status =@status, 

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

