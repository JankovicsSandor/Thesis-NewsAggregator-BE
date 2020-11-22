using System.Collections.Generic;

namespace Reader.DataAccess.Database
{
    public interface IMongoDatabaseService
    {
        void AddArticleGroup(ArticleGroup newGroup);
        List<ArticleGroup> GetAllArticleGroup();
    }
}