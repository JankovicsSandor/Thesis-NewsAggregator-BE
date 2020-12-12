using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBusRabbitMQ.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceConfiguration.API.BussinessLogic.AddNewResource.Command;
using ResourceConfiguration.API.BussinessLogic.ResourceProperties.Command;
using ResourceConfiguration.API.Models.Input;
using ResourceConfigurator.Shared.Event;

namespace ResourceConfiguration.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private IMediator _mediator;

        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
 
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewConfiguration([FromBody] AddNewResourceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestResourceProperties([FromBody] RequestResourceProperties rssUrlModel)
        {
            return Ok(await _mediator.Send(new GetResourcePropertiesCommand() { Url = rssUrlModel.Url }));
        }
    }
}