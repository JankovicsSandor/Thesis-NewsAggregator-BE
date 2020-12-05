using MediatR;
using Reader.DataAccess.Database;
using Reader.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reader.BussinessLogic.Homepage
{
    public class GetHomePageArticleListHandler : IRequestHandler<GetHomePageArticleListQuery, IList<HomePageNewsGroup>>
    {
        private IMongoDatabaseService _database;

        public GetHomePageArticleListHandler(IMongoDatabaseService database)
        {
            _database = database;
        }
        public Task<IList<HomePageNewsGroup>> Handle(GetHomePageArticleListQuery request, CancellationToken cancellationToken)
        {
            //TODO write tests for this class
            List<ArticleGroup> homepage = _database.GetHomePageArticles(DateTime.Now.AddDays(-2));
            IList<HomePageNewsGroup> returnValue = new List<HomePageNewsGroup>();
            foreach (ArticleGroup oneArticleGroup in homepage)
            {
                HomePageNewsGroup articleGroup = new HomePageNewsGroup();
                foreach (Article oneArticle in oneArticleGroup.Similar)
                {
                    HomePageNewsItem actualArticle = new HomePageNewsItem()
                    {
                        Description = oneArticle.Description,
                        Link = oneArticle.Link,
                        Picture = oneArticle.Picture,
                        PublishDate = oneArticle.PublishDate,
                        Title = oneArticle.Title,
                        FeedName = oneArticle.FeedName,
                        FeedPicture = oneArticle.FeedPicture
                    };
                    articleGroup.NewsItem.Add(actualArticle);
                }
                returnValue.Add(articleGroup);
            }
            return Task.FromResult(returnValue);
        }
    }
}
