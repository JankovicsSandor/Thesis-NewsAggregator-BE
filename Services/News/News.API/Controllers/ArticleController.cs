using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.BussinessLogic.ArticleResource.AddArticle;
using News.BussinessLogic.ArticleResource.GetArticle;
using News.BussinessLogic.GetArticle;

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

        [HttpGet("today")]
        public async Task<IActionResult> GetTodayNewsArticles([FromQuery] GetArticleCommand command)
        {
            return Ok(await _meditor.Send(new GetArticleCommand()
            {
                MinDate = DateTime.Now.Date
            }));
        }


        /// <summary>
        /// Get an article item from the descripition.
        /// </summary>
        /// <param name="description">Description of the article item</param>
        /// <returns>Returns the article item</returns>
        [HttpGet("byDescription")]
        public async Task<IActionResult> GetArticleItemFromDescription([FromQuery] string description)
        {
            return Ok(await _meditor.Send(new GetArticleByDescriptionCommand()
            {
                Description = description
            }));
        }
    }
}
