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
    public class CommentController : ControllerBase
    {
        private readonly CommentService _cService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger, CommentService cService)
        {
            _cService = cService;
            _logger = logger;
        }

        [Route("getComments")]
        [HttpPost]
        public async Task<IEnumerable<Comment>> FilterCOmments([FromBody] CommentCriteria criteria)
        {
            var result = await _cService.FilterComment(criteria);
            return result;
        }
        


        [HttpPost("Add")]
        public async Task<bool> AddComment([FromBody] Comment comment)
        {
            var result = await _cService.PostComment(comment);
            return result;
        }

        [HttpDelete("deletecomment/{commentId}")]
        public async Task<string> DeleteComment([FromRoute] int catId)
        {

            var res = await _cService.DeleteComment(catId);
            return res;

        }



    }
}