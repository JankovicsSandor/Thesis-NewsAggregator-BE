using MediatR;
using News.API.BussinessLogic.GetArticle.Command;
using News.DataAccess.Database;
using News.Shared.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using static News.API.BussinessLogic.GetArticle.PredicateBuild.ArticlePredicateBuilder;

namespace News.API.BussinessLogic.GetArticle.Handler
{
    public class GetArticleCommandHandler : IRequestHandler<GetArticleCommand, ArticleListResponse>
    {
        private newsaggregatordataContext _context;
        private int _pageSize;

        public GetArticleCommandHandler(newsaggregatordataContext context)
        {
            _context = context;
            _pageSize = 50;
        }

        // TODO group result for thesis
        // TODO add pagesize enlarge option
        public Task<ArticleListResponse> Handle(GetArticleCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Article, bool>> articleQuery = ArticlePredicateQueryBuilder.GetArticleQuery(request);
            int skip = 0;

            //Set default value for the page. Everything below 2 is the first page
            if (request.Page < 2)
            {
                request.Page = 1;
            }

            if (request.Page != 1)
            {
                skip = (request.Page - 1) * _pageSize;
            }
            var list = (from article in _context.Article.Where(articleQuery)
                        join author in _context.Feed.Where(e => e.Active) on article.FeedId equals author.Id
                        orderby article.PublishDate descending
                        select new NewsResponse()
                        {
                            Author = new NewsAuthorResponse()
                            {
                                Picture = author.Picture
                            },
                            Picture = article.Picture,
                            Link = article.Link,
                            PublishDate = article.PublishDate,
                            Description = article.Description,
                            Title = article.Title
                        });
            int total = list.Count();
            list = list.Skip(skip).Take(_pageSize);
            var returnList = new ArticleListResponse()
            {
                Result = list,
                Total = total
            };
            return Task.FromResult(returnList);

        }
    }
}
