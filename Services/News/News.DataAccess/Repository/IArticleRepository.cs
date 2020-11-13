using News.DataAccess.Database;
using System.Threading.Tasks;

namespace News.DataAccess.Repository
{
    public interface IArticleRepository
    {
        Task AddNewArticle(Article newArticle);
    }
}