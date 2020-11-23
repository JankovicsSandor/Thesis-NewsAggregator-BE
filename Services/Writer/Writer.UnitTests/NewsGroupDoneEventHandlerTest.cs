using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Writer.BussinessLogic.EventHandler;
using Writer.BussinessLogic.ExternalDataProvider.NewsData;
using Writer.DataAccess.Database;
using Writer.Shared.Events;

namespace Writer.UnitTests
{
    public class NewsGroupDoneEventHandlerTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Handle_Test_When_NullParam_Is_Given()
        {
            Assert.Throws<ArgumentNullException>(() => new NewsGroupDoneEventHandler(null, null));
        }

        [Test]
        public async Task Handle_Test_When_Correct_Param_Is_Given_Fresh_Database()
        {
            Mock<INewsHttpClient> newsHttpClient = new Mock<INewsHttpClient>();
            Mock<IMongoDatabaseService> mongoService = new Mock<IMongoDatabaseService>();
            NewsGroupDoneEventHandler handler = new NewsGroupDoneEventHandler(newsHttpClient.Object, mongoService.Object);

            await handler.Handle(new NewsGroupDoneEvent()
            {
                NewsItem = new NewsGroupDoneNewsItem()
            });
            newsHttpClient.Verify(e => e.GetArticleFromDescription(It.IsAny<string>()), Times.Never);
            mongoService.Verify(e => e.AddArticleGroup(It.IsAny<ArticleGroup>()), Times.Once);
        }
    }
}
