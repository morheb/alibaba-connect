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
    public class AddonController : ControllerBase
    {
        private readonly AddonService _pService;
        private readonly ILogger<AddonController> _logger;

        public AddonController(ILogger<AddonController> logger, AddonService pService)
        {
            _pService = pService;
            _logger = logger;
        }

        [Route("getaddon/{id}")]
        [HttpGet]
        public async Task<Addon> GetAddonById([FromRoute] int id)
        {
            var result = await _pService.GetAddonById(id);
            return result;
        }


        [HttpPost("Add")]
        public async Task<bool> RegisterAddon([FromBody] Addon addon)
        {
            var result = await _pService.PostAddon(addon);
            return result;
        }

        [HttpPut("updateAddon")]
        public async Task<string> Updateaddon([FromBody] Addon addon)
        {
            var result = await _pService.GetAddonById(addon.Id);
            if (result != null)
            {
                await _pService.UpdateAddon(addon);
                return "success";
            }

            return "addon doesnt exisit";

        }
   

        [HttpDelete("delete/{id}")]
        public async Task<string> Updateaddon([FromRoute] int id)
        {
            var result = await _pService.GetAddonById(id);
            if (result != null)
            {
                await _pService.DeleteAddon(id);
                return "success";
            }

            return "addon doesnt exisit";

        }
   
      


        [HttpPost("FilterAddon")]
        public async Task<IEnumerable<Addon>> FilterAddons([FromBody] AddonCriteria criteria)
        {

            var res = await _pService.FilterAddon(criteria);
            return res;

        }


    }
}