using News.DataAccess.Database;
using System.Collections.Generic;

namespace Writer.DataAccess.Database
{
    public interface IMongoDatabaseService
    {
        void AddArticleGroup(ArticleGroup newGroup);
        List<ArticleGroup> GetAllArticleGroup();
    }
}