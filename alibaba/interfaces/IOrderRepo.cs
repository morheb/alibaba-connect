﻿using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface IOrderRepo
    {
        Task<DbOrder> GetOrderByIdAsync(int orderId);
        Task<int> GetOrdersStatistics(DbOrderCriteria criteria);
        Task<DbResponse> PostOrderAsync(DbOrder order);
        Task<bool> UpdateOrderDriver(int orderId, int driverId);

        Task<bool> UpdateOrderStatusAsync(DbOrderStatus order );
        Task<IEnumerable<DbOrder>> GetShopOrders(int shopId);
        Task<IEnumerable<DbOrder>> FilterOrders(DbOrderCriteria criteria);
    }
}
