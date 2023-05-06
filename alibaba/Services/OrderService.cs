

namespace alibaba
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using MySql.Data;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Threading.Tasks;
    using alibaba.Data;
    using alibaba.interfaces;
    using alibaba.Services.Models;

    public class OrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _pRepo;

        public OrderService(IOrderRepo pRepo, IMapper mapper)
        {
            _pRepo = pRepo;
            _mapper = mapper;

        }
        public async Task<Order> GetOrderById(int id)
        {

            var res = await _pRepo.GetOrderByIdAsync(id);
            if (res != null)
            {
                var rest = _mapper.Map<Order>(res);
                return rest;
            }
            return null;
        }
        
        public async Task<int> GetOrderStatistics(OrderCriteria criteria)
        {
            DbOrderCriteria dbCriteria = _mapper.Map<DbOrderCriteria>(criteria);


            var res = await _pRepo.GetOrdersStatistics(dbCriteria);
   
            return res;
        }
        
        public async Task<Response> PostOrder(Order order)
        {
            DbOrder dbRest;
            var dbOrder = _mapper.Map<DbOrder>(order);
    
            //var dbOrder = this.ToDbOrder(order);
            var res = await _pRepo.PostOrderAsync(dbOrder);
            var response = _mapper.Map<Response>(res);

            return response;
        }

        public async Task<bool> UpdateOrderStatus(OrderStatus status)
        {
            var dborder = _mapper.Map<DbOrderStatus>(status);
            var res = await _pRepo.UpdateOrderStatusAsync(dborder);

            return res;
        }
        public async Task<bool> UpdateOrderDriver(int orderId, int  driverId)
        {
            var res = await _pRepo.UpdateOrderDriver(orderId, driverId);

            return res;
        }
        //public async Task<bool>SetOrderStatus(OrderStatus status )
        //{
        //    DbOrderStatus statusDb = _mapper.Map<DbOrderStatus>(status);
        //    var res = await _pRepo.SetOrderStatusAsync(statusDb );

        //    return res;
        //}

        public async Task<IEnumerable<Order>> FilterOrder(OrderCriteria criteria)
        {
            DbOrderCriteria dbCriteria = _mapper.Map<DbOrderCriteria>(criteria);
            var res = await _pRepo.FilterOrders(dbCriteria);
            IEnumerable<Order> orders = _mapper.Map<IEnumerable<Order>>(res);

            return orders;
        }

        private Order ToOrder (DbOrder dbOrder)
        {
            List<ProductOrder> po = new List<ProductOrder>();
            foreach (DbProductOrder p in dbOrder.Products)
            {
                po.Add(new ProductOrder()
                {
                    Count = p.Count,
                    Id = p.Id
                });
            }
            return new Order()
            {
                DriverId = dbOrder.DriverId,
                Price = dbOrder.Price,
                Status = dbOrder.Status,
                Products = po
            };
        }
        private DbOrder ToDbOrder (Order order)
        {
            List<DbProductOrder> po = new List<DbProductOrder>();
            foreach (ProductOrder p in order.Products )
            {
                po.Add(new DbProductOrder()
                {
                    Count = p.Count,
                    Id = p.Id
                });
            }
            return new DbOrder()
            {
                DriverId = order.DriverId,
                Price = order.Price,
                Status = order.Status,
                Products = po
            };
        }


    }
}
