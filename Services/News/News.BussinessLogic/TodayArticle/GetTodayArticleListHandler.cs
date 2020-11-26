using MediatR;
using News.BussinessLogic.ArticleResource;
using News.DataAccess.Database;
using News.DataAccess.Repository;
using News.Shared.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace News.BussinessLogic.TodayArticle
{
    public class GetTodayArticleListHandler : IRequestHandler<GetTodayArticleListQuery, IList<string>>
    {
        private IArticleRepository _articleRepo;

        public GetTodayArticleListHandler(IArticleRepository articleRepository)
        {
            _articleRepo = articleRepository;
        }
        public async Task<IList<string>> Handle(GetTodayArticleListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Article, bool>> articleQuery = ArticlePredicateQueryBuilder.GetArticleQuery(new ArticleQueryModel()
            {
                MinDate = DateTime.Now.Date.AddDays(-1)
            });

            IList<string> articleList = _articleRepo.GetArticleFromQuery(articleQuery).Select(item => item.Description).ToList();

            return articleList;
        }
    }
}
