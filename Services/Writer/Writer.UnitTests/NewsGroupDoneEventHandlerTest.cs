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
using Writer.Shared.External;
using Writer.UnitTests.Data;

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

        [Test]
        public async Task Handle_Test_When_Correct_Param_Is_Given_Database_Contains_Data()
        {

            Mock<INewsHttpClient> newsHttpClient = new Mock<INewsHttpClient>();
            Mock<IMongoDatabaseService> mongoService = new Mock<IMongoDatabaseService>();

            mongoService.Setup(e => e.GetArticleGroupsFromDateTime(It.IsAny<DateTime>())).Returns(ArticleGroupTestData.GetArticleGroupList());
            newsHttpClient.Setup(e => e.GetArticleFromDescription(It.IsAny<string>())).ReturnsAsync(new GetNewsArticleByDescriptionResponse() {Guid= "aaaa-4444-aaaa" });

            NewsGroupDoneEventHandler handler = new NewsGroupDoneEventHandler(newsHttpClient.Object, mongoService.Object);

            await handler.Handle(new NewsGroupDoneEvent()
            {
                NewsItem = new NewsGroupDoneNewsItem(),
                Similarities = new List<string>()
                {
                    "Item2"
                }
            });
            newsHttpClient.Verify(e => e.GetArticleFromDescription(It.IsAny<string>()), Times.Once);
            mongoService.Verify(e => e.AddArticleGroup(It.IsAny<ArticleGroup>()), Times.Never);
            mongoService.Verify(e => e.UpdateArticleGroup(It.IsAny<ArticleGroup>()), Times.Once);
        }

        [Test]
        public async Task Handle_Test_When_Correct_Param_Is_Given_Database_Contains_Data_But_Not_Similar()
        {

            Mock<INewsHttpClient> newsHttpClient = new Mock<INewsHttpClient>();
            Mock<IMongoDatabaseService> mongoService = new Mock<IMongoDatabaseService>();

            mongoService.Setup(e => e.GetArticleGroupsFromDateTime(It.IsAny<DateTime>())).Returns(ArticleGroupTestData.GetArticleGroupList());
            newsHttpClient.Setup(e => e.GetArticleFromDescription(It.IsAny<string>())).ReturnsAsync(new GetNewsArticleByDescriptionResponse() { Guid = "bbbb-4444-bbbb" });

            NewsGroupDoneEventHandler handler = new NewsGroupDoneEventHandler(newsHttpClient.Object, mongoService.Object);

            await handler.Handle(new NewsGroupDoneEvent()
            {
                NewsItem = new NewsGroupDoneNewsItem(),
                Similarities = new List<string>()
                {
                    "Item2"
                }
            });
            newsHttpClient.Verify(e => e.GetArticleFromDescription(It.IsAny<string>()), Times.Once);
            mongoService.Verify(e => e.AddArticleGroup(It.IsAny<ArticleGroup>()), Times.Once);
            mongoService.Verify(e => e.UpdateArticleGroup(It.IsAny<ArticleGroup>()), Times.Never);
        }
    }
}
