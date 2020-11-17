using Microsoft.EntityFrameworkCore;
using News.DataAccess.Database;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DataAccess.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private newsaggregatordataContext _context;

        public ArticleRepository(newsaggregatordataContext context)
        {
            _context = context;
        }

        public async Task AddNewArticle(Article newArticle)
        {
            await _context.Article.AddAsync(newArticle);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<NewsItemResponse> GetArticleByDescription(string description)
        {
            return (from articles in _context.Article.Where(e => e.Description == description)
                    join feed in _context.Feed on articles.FeedId equals feed.Id
                    select new NewsItemResponse()
                    {
                        Description = articles.Description,
                        FeedName = feed.Name,
                        FeedPicture = feed.Picture,
                        Picture = articles.Picture,
                        Guid = articles.Guid,
                        Link = articles.Link,
                        PublishDate = articles.PublishDate,
                        Title = articles.Title
                    });
        }
    }
}
