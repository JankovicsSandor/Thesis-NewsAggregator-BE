using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.BussinessLogic.ArticleResource.GetArticleByGUID;
using News.BussinessLogic.TodayArticle;

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



        [HttpGet("today")]
        public async Task<IActionResult> GetTodayNewsArticles()
        {
            
            return Ok(await _meditor.Send(new GetTodayArticleListQuery()));
        }


        /// <summary>
        /// Get an article item from the descripition.
        /// </summary>
        /// <param name="description">Description of the article item</param>
        /// <returns>Returns the article item</returns>
        [HttpGet("byGuid")]
        public async Task<IActionResult> GetArticleItemFromDescription([FromQuery] string guid)
        {
            return Ok(await _meditor.Send(new GetArticleByGUIDCommand()
            {
                GuId = guid
            }));
        }
    }
}
