using System;
using System.Collections.Generic;

namespace Reader.DataAccess.Database
{
    public interface IMongoDatabaseService
    {
        List<ArticleGroup> GetAllArticleGroup();
        List<ArticleGroup> GetHomePageArticles(DateTime minDate);
    }
}