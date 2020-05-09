using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.API.BussinessLogic.AddNewArticle.Command;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IMediator _meditor;

        public ArticleController(IMediator mediator)
        {
            _meditor = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetArticles()
        {
            return Ok("pong");
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewArticle([FromBody] AddNewArticleCommand newArticle)
        {
            await _meditor.Send(newArticle);
            return Ok();
        }
    }
}
