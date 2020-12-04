using MediatR;
using News.BussinessLogic.ArticleResource;
using News.DataAccess.Database;
using News.DataAccess.Repository;
using News.Shared.Query;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace News.BussinessLogic.TodayArticle
{
    public class GetTodayArticleListHandler : IRequestHandler<GetTodayArticleListQuery, IList<NewsListModel>>
    {
        private IArticleRepository _articleRepo;

        public GetTodayArticleListHandler(IArticleRepository articleRepository)
        {
            _articleRepo = articleRepository;
        }
        public async Task<IList<NewsListModel>> Handle(GetTodayArticleListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Article, bool>> articleQuery = ArticlePredicateQueryBuilder.GetArticleQuery(new ArticleQueryModel()
            {
                MinDate = DateTime.Now.Date.AddDays(-1)
            });

            IList<NewsListModel> articleList = _articleRepo.GetArticleFromQuery(articleQuery).Select(item => new NewsListModel()
            {
                GUID=item.Guid,
                Description=item.Description,
            }).ToList();

            return articleList;
        }
    }
}
