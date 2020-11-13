using News.DataAccess.Database;
using System;
using System.Collections.Generic;
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
    }
}
