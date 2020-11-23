using System;
using System.Collections.Generic;

namespace Writer.DataAccess.Database
{
    public interface IMongoDatabaseService
    {
        void AddArticleGroup(ArticleGroup newGroup);
        List<ArticleGroup> GetAllArticleGroup();
        List<ArticleGroup> GetArticleGroupsFromDateTime(DateTime minDate);
        void UpdateArticleGroup(ArticleGroup updatedGroup);
    }
}