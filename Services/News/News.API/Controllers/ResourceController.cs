using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.API.BussinessLogic.AddNewResource.Command;
using ResourceConfigurator.Shared.Event;

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("")]
        public async Task<IActionResult> AddNewResource([FromBody]AddNewResourceCommand eventModel)
        {
            return Ok(await _mediator.Send(eventModel));
        }
    }
}