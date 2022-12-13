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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _cService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, CategoryService cService)
        {
            _cService = cService;
            _logger = logger;
        }

        [Route("getcategories/{restaurantid}")]
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategoryById([FromRoute] int restaurantid)
        {
            var result = await _cService.FilterCategory(restaurantid);
            return result;
        }
        


        [HttpPost("Add")]
        public async Task<bool> RegisterCategory([FromBody] Category category)
        {
            var result = await _cService.PostCategory(category);
            return result;
        }

        [HttpPut("updateCategory")]
        public async Task<string> Updatecategory([FromBody] Category category)
        {
            try
            {
                await _cService.UpdateCategory(category);
                return "success";

            }catch(Exception e)
            {
                return e.Message;

            }

        }
        [HttpDelete("deletecategory/{catId}")]
        public async Task<string> DeleteCategory([FromRoute] int catId)
        {

            var res = await _cService.DeleteCategory(catId);
            return res;

        }



    }
}