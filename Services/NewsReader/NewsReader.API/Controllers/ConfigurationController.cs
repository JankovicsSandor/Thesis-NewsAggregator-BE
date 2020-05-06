using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsReader.API.BussinessLogic.ResourceProperties.Command;
using NewsReader.API.Models.Input;

namespace NewsReader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private IMediator _mediator;

        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> AddNewConfiguration()
        {
            return Ok(new { Ping = "pong" });
        }


        [HttpPost("request")]
        public async Task<IActionResult> RequestResourceProperties([FromBody] RequestResourceProperties rssUrlModel)
        {
            return Ok(await _mediator.Send(new GetResourcePropertiesCommand() { Url = rssUrlModel.Url }));
        }
    }
}