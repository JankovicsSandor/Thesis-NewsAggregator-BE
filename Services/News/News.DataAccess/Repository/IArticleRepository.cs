using News.DataAccess.Database;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News.DataAccess.Repository
{
    public interface IArticleRepository
    {
        Task AddNewArticle(Article newArticle);
        IEnumerable<NewsItemResponse> GetArticleByDescription(string description);
        IQueryable<NewsResponse> GetArticleFromQuery(Expression<Func<Article, bool>> articleQuery);
    }
}