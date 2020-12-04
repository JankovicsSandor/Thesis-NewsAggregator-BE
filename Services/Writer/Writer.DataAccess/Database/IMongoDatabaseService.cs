using System;
using System.Collections.Generic;

namespace Writer.DataAccess.Database
{
    public interface IMongoDatabaseService
    {
        void AddArticleGroup(ArticleGroup newGroup);
        IList<ArticleGroup> GetAllArticleGroup();
        Article GetArticleFromGUID(string guid);
        IList<ArticleGroup> GetArticleGroupsFromDateTime(DateTime minDate);
        void UpdateArticleGroup(ArticleGroup updatedGroup);
    }
}