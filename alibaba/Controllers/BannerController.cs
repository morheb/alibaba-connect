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
    public class BannerController : ControllerBase
    {
        private readonly BannerService _bService;
        private readonly ILogger<BannerController> _logger;

        public BannerController(ILogger<BannerController> logger, BannerService cService)
        {
            _bService = cService;
            _logger = logger;
        }

        [Route("getbanner/{id}")]
        [HttpGet]
        public async Task<string> GetBannerById([FromRoute] int id)
        {
            var result = await _bService.GetBannerById(id);
            return result;
        }
        


        [HttpPost("Add")]
        public async Task<bool> PostBanner([FromBody] Banner request)
        {
            var result = await _bService.PostBanner(request);
            return result;
        }



        [HttpDelete("deleteBanner/{id}")]
        public async Task<string> Deletebanner([FromRoute] int id)
        {
            try
            {
                await _bService.DeleteBanner(id);
                return "success";

            }catch(Exception e)
            {
                return e.Message;

            }

        }

       
        [HttpGet("getAllBanner")]
        public async Task<IEnumerable<Banner>> GetAllBanners()
        {
            try
            {
               var res= await _bService.GetAllBannersAsync();
                return res;

            }catch(Exception e)
            {
                return null;

            }

        }

       
        
        [HttpPut("updateBanner")]
        public async Task<string> Updatebanner([FromBody] Banner request)
        {
            try
            {
                await _bService.UpdateBanner(request);
                return "success";

            }catch(Exception e)
            {
                return e.Message;

            }

        }

       


    }
}