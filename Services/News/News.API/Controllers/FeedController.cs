using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BussinessLogic.GetFeed;
using News.Shared.Models;

namespace News.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private IMediator _meditor;
        public FeedController(IMediator mediator)
        {
            _meditor = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedPropertiesAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Error = "Entity id must be greater than zero" });
            }
            FeedModel requestedFeed = await _meditor.Send(new GetFeedPropertiesQuery()
            {
                FeedId = id
            });

            if (requestedFeed == null)
            {
                return BadRequest(new { Error = "Queried entity not found" });
            }

            return Ok(requestedFeed);
        }
    }
}
