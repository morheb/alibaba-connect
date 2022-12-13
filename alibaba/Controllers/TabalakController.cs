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
    public class TalabakController : ControllerBase
    {
        private readonly TalabakService _oService;
        private readonly ILogger<TalabakController> _logger;

        public TalabakController(ILogger<TalabakController> logger, TalabakService oService)
        {
            _oService = oService;
            _logger = logger;
        }

        [Route("gettalabak/{id}")]
        [HttpGet]
        public async Task<Talabak> GetTalabakById([FromRoute] int id)
        {
            var result = await _oService.GetTalabakById(id);
            return result;
        }
        
        [Route("gettalabakstatistics")]
        [HttpPost]
        public async Task<int> GetTalabaksStaistics([FromBody] TalabakCriteria criteria)
        {
            var result = await _oService.GetTalabakStatistics(criteria);
            return result;
        }


        [HttpPost("Add")]
        public async Task<Response> RegisterTalabak([FromBody] Talabak product)
        {
            var result = await _oService.PostTalabak(product);
            return result;
        }

        [HttpPut("updateTalabakStatus")]
        public async Task<string> Updateproduct([FromBody] TalabakStatus product)
        {
            var result = await _oService.GetTalabakById(product.Id);
            if (result != null)
            {
                await _oService.UpdateTalabakStatus(product);
                return "success";
            }

            return "talabak doesnt exisit";

        }

        //[HttpPut("setTalabakstatus")]
        //public async Task<bool> updateRes([FromBody] TalabakStatus status)
        //{
        //    var result = await _rService.GetTalabakById(status.Id);
        //    if (result == null)
        //    {
        //        await _rService.SetTalabakStatus(status);
        //        return false;
        //    }

        //    return true;

        //}

        [HttpPost("FilterTalabak")]
        public async Task<IEnumerable<Talabak>> FilterTalabaks([FromBody] TalabakCriteria criteria)
        {

            var res = await _oService.FilterTalabak(criteria);
            return res;

        }


    }
}