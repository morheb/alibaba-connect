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
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _rService;
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger, RestaurantService rService)
        {
            _rService = rService;
            _logger = logger;
        }

        [Route("getrestaurant/{id}")]
        [HttpGet]
        public async Task<Restaurant> GetRestaurantById([FromRoute] int id)
        {
            var result = await _rService.GetRestaurantById(id);
            return result;
        }

        [Route("getmyrestaurant/{id}")]
        [HttpGet]
        public async Task<Restaurant> GetMyRestaurantById([FromRoute] string id)
        {
            var result = await _rService.GetMyRestaurantById(id);
            return result;
        }


        [HttpPost("Add")]
        public async Task<Response> RegisterRestaurant([FromBody] Restaurant restaurant)
        {
            var result = await _rService.PostRestaurant(restaurant);
            return result;
        }

        [HttpPut("updateRestaurant")]
        public async Task<string> Updaterestaurant([FromBody] Restaurant restaurant)
        {
            var result = await _rService.GetRestaurantById(restaurant.Id);
            if (result != null)
            {
                await _rService.UpdateRestaurant(restaurant);
                return "success";
            }

            return "restaurant doesnt exisit";

        }

        [HttpPut("setRestaurantstatus")]
        public async Task<bool> updateRes([FromBody] RestaurantStatus status)
        {
            var result = await _rService.GetRestaurantById(status.Id);
            if (result != null)
            {
                await _rService.SetRestaurantStatus(status);
                return true;
            }

            return false;

        } 

        [HttpPost("FilterRestaurant")]
        public async Task<IEnumerable<Restaurant>> FilterRestaurants([FromBody] RestaurantCriteria criteria)
        {

            var res = await _rService.FilterRestaurant(criteria);
            return res;

        }
      

    }
}