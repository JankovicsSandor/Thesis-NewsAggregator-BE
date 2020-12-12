using Moq;
using News.BussinessLogic.ArticleResource.GetArticleByGUID;
using News.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace News.Test
{
    public class GetArticleByGuidHandlerTest
    {

        [Fact]
        public void GetArticleByGUIDNullRequestThrowsExcpetion()
        {
            Mock<IArticleRepository> mockArticle = new Mock<IArticleRepository>();
            GetArticleByGUIDHandler testHandler = new GetArticleByGUIDHandler(mockArticle.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => testHandler.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
