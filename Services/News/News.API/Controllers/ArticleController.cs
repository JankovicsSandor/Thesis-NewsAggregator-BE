using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.BussinessLogic.ArticleResource.AddArticle;
using News.BussinessLogic.ArticleResource.GetArticle;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IMediator _meditor;

        public ArticleController(IMediator mediator)
        {
            _meditor = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetArticles([FromQuery] GetArticleCommand command)
        {

            return Ok(await _meditor.Send(command));
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewArticle([FromBody] AddNewArticleCommand newArticle)
        {
            await _meditor.Send(newArticle);
            return Ok();
        }
    }
}
