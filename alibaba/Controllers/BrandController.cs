using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;
using alibaba.Services.Models;

namespace alibaba.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _cService;
        private readonly ILogger<BrandController> _logger;

        public BrandController(ILogger<BrandController> logger, BrandService bService)
        {
            _cService = bService;
            _logger = logger;
        }

        [Route("getcategories/{restaurantid}")]
        [HttpGet]
        public async Task<IEnumerable<Brand>> GetBrandById([FromRoute] int restaurantid)
        {
            var result = await _cService.FilterBrand(restaurantid);
            return result;
        }
        


        [HttpPost("Add")]
        public async Task<bool> RegisterBrand([FromBody] Brand brand)
        {
            var result = await _cService.PostBrand(brand);
            return result;
        }

        [HttpPut("updateBrand")]
        public async Task<string> Updatebrand([FromBody] Brand brand)
        {
            try
            {
                await _cService.UpdateBrand(brand);
                return "success";

            }catch(Exception e)
            {
                return e.Message;

            }

        }
        [HttpDelete("deletebrand/{catId}")]
        public async Task<string> DeleteBrand([FromRoute] int catId)
        {

            var res = await _cService.DeleteBrand(catId);
            return res;

        }



    }
}