using News.DataAccess.Database;
using News.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.DataAccess.Repository
{
    public interface IArticleRepository
    {
        Task AddNewArticle(Article newArticle);
        IEnumerable<NewsItemResponse> GetArticleByDescription(string description);
    }
}