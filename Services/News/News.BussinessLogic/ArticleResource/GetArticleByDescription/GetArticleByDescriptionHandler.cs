using MediatR;
using News.BussinessLogic.GetArticle;
using News.DataAccess.Repository;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace News.BussinessLogic.ArticleResource.GetArticleByDescription
{
    /// <summary>
    /// Handler class of the getarticle controller. This class handles the request coming from the writer api to get the article item 
    /// from description
    /// </summary>
    public class GetArticleByDescriptionHandler : IRequestHandler<GetArticleByDescriptionCommand, NewsItemResponse>
    {
        private IArticleRepository _articleRepository;

        /// <summary>
        /// Creates a new instance of GetArticleByDescriptionHandler
        /// </summary>
        /// <param name="articleRepository">Data access for the articles</param>
        public GetArticleByDescriptionHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }

        /// <summary>
        /// Handles the mediator raiesd GetArticleByDescriptionCommand event.
        /// </summary>
        /// <param name="request">The description query for the article</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the first matching article item based on the description.</returns>
        public Task<NewsItemResponse> Handle(GetArticleByDescriptionCommand request, CancellationToken cancellationToken)
        {
            NewsItemResponse actualArticle = _articleRepository.GetArticleByDescription(request.Description).FirstOrDefault();
            return Task.FromResult(actualArticle);
        }
    }
}
