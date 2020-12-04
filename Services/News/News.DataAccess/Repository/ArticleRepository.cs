using Microsoft.EntityFrameworkCore;
using News.DataAccess.Database;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<NewsItemResponse> GetArticleByGuid(string guid)
        {
            return (from articles in _context.Article.Where(e => e.Guid == guid)
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

        public IQueryable<NewsResponse> GetArticleFromQuery(Expression<Func<Article, bool>> articleQuery)
        {
            return (from article in _context.Article.Where(articleQuery)
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
                        Guid = article.Guid,
                        PublishDate = article.PublishDate,
                        Description = article.Description,
                        Title = article.Title
                    }) ;
        }
    }
}
