using System;
using System.Collections.Generic;
using System.Text;
using Writer.DataAccess.Database;

namespace Writer.UnitTests.Data
{
    public static class ArticleGroupTestData
    {
        public static IList<ArticleGroup> GetArticleGroupList()
        {
            return new List<ArticleGroup>() {
                new ArticleGroup() {
                    CreateDate=new DateTime(2012,12,12),
                    LatestArticleDate=new DateTime(2018,2,1),
                    Similar=new List<Article>(){
                        new Article()
                        {
                           Description="It is bad",
                           FeedName="Item1",
                           FeedPicture="Picture1",
                           Link="Linkaaaa",
                           NewsID="aaaa-4444-aaaa",
                           PublishDate=new DateTime(2018,2,1),
                           Picture="pictureLink",
                           Title="This is an article"
                        }
                    }
                }
            };
        }
    }
}
