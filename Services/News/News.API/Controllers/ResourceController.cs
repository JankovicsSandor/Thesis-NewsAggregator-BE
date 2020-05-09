using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceConfigurator.Shared.Event;

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        public ResourceController()
        {

        }


        public async Task<IActionResult> AddNewResource([FromBody]AddNewResourceEvent eventModel)
        {
            return Ok();
        }
    }
}