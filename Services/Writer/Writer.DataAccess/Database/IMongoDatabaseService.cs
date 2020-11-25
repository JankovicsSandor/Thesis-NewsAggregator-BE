using System;
using System.Collections.Generic;

namespace Writer.DataAccess.Database
{
    public interface IMongoDatabaseService
    {
        void AddArticleGroup(ArticleGroup newGroup);
        IList<ArticleGroup> GetAllArticleGroup();
        IList<ArticleGroup> GetArticleGroupsFromDateTime(DateTime minDate);
        void UpdateArticleGroup(ArticleGroup updatedGroup);
    }
}