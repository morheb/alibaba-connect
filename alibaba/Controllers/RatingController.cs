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
    public class RatingController : ControllerBase
    {
        private readonly RatingService _pService;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger, RatingService pService)
        {
            _pService = pService;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<bool> PostRating([FromBody] Rating rating)
        {
            var result = await _pService.PostRatingAsync(rating);
            return result;
        }
    }
}