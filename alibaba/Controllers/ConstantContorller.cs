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
    public class ConstantController : ControllerBase
    {
        private readonly ConstantService _cService;
        private readonly ILogger<ConstantController> _logger;

        public ConstantController(ILogger<ConstantController> logger, ConstantService cService)
        {
            _cService = cService;
            _logger = logger;
        }

        [Route("getconstant/{id}")]
        [HttpGet]
        public async Task<double> GetConstantById([FromRoute] int id)
        {
            var result = await _cService.GetConstantById(id);
            return result;
        }
        


        [HttpPost("Add")]
        public async Task<bool> PostConstant([FromBody] NewConstantRequest request)
        {
            var result = await _cService.PostConstant(request);
            return result;
        }

        [HttpPut("updateConstant")]
        public async Task<string> Updateconstant([FromBody] ChangeConstantRequest request)
        {
            try
            {
                await _cService.UpdateConstant(request);
                return "success";

            }catch(Exception e)
            {
                return e.Message;

            }

        }

       


    }
}