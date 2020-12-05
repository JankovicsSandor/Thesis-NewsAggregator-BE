using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reader.BussinessLogic.Homepage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private IMediator _mediator;

        public NewsController(IMediator meditor)
        {
            _mediator = meditor;
        }

        [HttpGet("homepage")]
        public async Task<IActionResult> GetHomePageArticleData()
        {

            return Ok(await _mediator.Send(new GetHomePageArticleListQuery()));
        }
    }
}
