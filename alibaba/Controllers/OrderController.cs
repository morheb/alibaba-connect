using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;
using alibaba.Services.Models;

namespace alibaba.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _oService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, OrderService oService)
        {
            _oService = oService;
            _logger = logger;
        }

        [Route("getorder/{id}")]
        [HttpGet]
        public async Task<Order> GetOrderById([FromRoute] int id)
        {
            var result = await _oService.GetOrderById(id);
            return result;
        }
        
        [Route("getorderstatistics")]
        [HttpPost]
        public async Task<int> GetOrdersStaistics([FromBody] OrderCriteria criteria)
        {
            var result = await _oService.GetOrderStatistics(criteria);
            return result;
        }


        [HttpPost("Add")]
        public async Task<Response> RegisterOrder([FromBody] Order product)
        {
            var result = await _oService.PostOrder(product);
            return result;
        }

        [HttpPut("updateOrderStatus")]
        public async Task<string> Updateproduct([FromBody] OrderStatus product)
        {
            var result = await _oService.GetOrderById(product.Id);
            if (result != null)
            {
                await _oService.UpdateOrderStatus(product);
                return "success";
            }

            return "product doesnt exisit";

        }
        [HttpPut("updateOrderDriver/{orderId}/{driverId}")]
        public async Task<string> UpdateOerderDriver([FromRoute] int orderId, [FromRoute]  int driverId)
        {
            var result = await _oService.GetOrderById(orderId);
            if (result != null)
            {
                
                await _oService.UpdateOrderDriver(orderId, driverId);
                return "success";
            }

            return "product doesnt exisit";

        }

        //[HttpPut("setOrderstatus")]
        //public async Task<bool> updateRes([FromBody] OrderStatus status)
        //{
        //    var result = await _rService.GetOrderById(status.Id);
        //    if (result == null)
        //    {
        //        await _rService.SetOrderStatus(status);
        //        return false;
        //    }

        //    return true;

        //}

        [HttpPost("FilterOrder")]
        public async Task<IEnumerable<Order>> FilterOrders([FromBody] OrderCriteria criteria)
        {

            var res = await _oService.FilterOrder(criteria);
            return res;

        }


    }
}