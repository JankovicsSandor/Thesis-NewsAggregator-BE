using Moq;
using News.BussinessLogic.GetFeed;
using News.DataAccess.Repository;
using System;
using System.Threading;
using Xunit;

namespace News.Test
{
    public class GetFeedPropertiesQueryHandlerTest
    {
        [Fact]
        public void GetFeedPropertiesQueryHandler_Throws_Error_On_Null_Input()
        {
            Mock<IFeedRepository> mockFeedRepo = new Mock<IFeedRepository>();

            GetFeedPropertiesQueryHandler handler = new GetFeedPropertiesQueryHandler(mockFeedRepo.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}
