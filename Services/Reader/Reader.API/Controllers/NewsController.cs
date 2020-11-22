using Microsoft.AspNetCore.Mvc;
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
        public NewsController()
        {

        }

        [HttpGet("")]
        public IActionResult GetNewsArticles()
        {
            return Ok();
        }
    }
}
