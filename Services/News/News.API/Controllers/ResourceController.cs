using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.BussinessLogic.AddNewResource;

namespace News.API.Controllers
{
    [Route("[controller]")]
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