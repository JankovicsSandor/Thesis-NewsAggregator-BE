using Moq;
using Reader.BussinessLogic.Homepage;
using Reader.DataAccess.Database;
using Reader.Shared.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Reader.Test
{
    public class GetHomePageArticleListHandlerTest
    {
        [Fact]
        public async Task GetHomePageArticleListWhenDatabaseIsEmpty()
        {
            Mock<IMongoDatabaseService> database = new Mock<IMongoDatabaseService>();

            database.Setup(e => e.GetHomePageArticles(It.IsAny<DateTime>())).Returns(new List<ArticleGroup>());

            GetHomePageArticleListHandler handler = new GetHomePageArticleListHandler(database.Object);

            IList<HomePageNewsGroup> homepageResult = await handler.Handle(new GetHomePageArticleListQuery(), It.IsAny<CancellationToken>());

            Assert.Equal(0, homepageResult.Count);
        }

        [Fact]
        public async Task GetHomePageArticleListWhenDatabaseHasValue()
        {
            Mock<IMongoDatabaseService> database = new Mock<IMongoDatabaseService>();

            database.Setup(e => e.GetHomePageArticles(It.IsAny<DateTime>())).Returns(new List<ArticleGroup>()
            {
                new ArticleGroup()
                {
                    CreateDate=DateTime.Now,
                    LatestDate=DateTime.Now,
                    Similar=new List<Article>()
                    {
                        new Article()
                        {
                            Description="Item1",
                            Title="Item1"
                        },
                         new Article()
                        {
                            Description="Item2",
                            Title="Item2"
                        }
                    }
                }
            });

            GetHomePageArticleListHandler handler = new GetHomePageArticleListHandler(database.Object);

            IList<HomePageNewsGroup> homepageResult = await handler.Handle(new GetHomePageArticleListQuery(), It.IsAny<CancellationToken>());

            Assert.Equal(1, homepageResult.Count);
            Assert.Equal("Item2", homepageResult[0].NewsItem[1].Description);
        }
    }
}
