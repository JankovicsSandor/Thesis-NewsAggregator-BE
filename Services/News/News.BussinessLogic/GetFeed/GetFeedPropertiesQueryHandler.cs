using MediatR;
using News.DataAccess.Repository;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace News.BussinessLogic.GetFeed
{
    public class GetFeedPropertiesQueryHandler : IRequestHandler<GetFeedPropertiesQuery, FeedModel>
    {
        private IFeedRepository _feedRepository;

        public GetFeedPropertiesQueryHandler(IFeedRepository feedRepository)
        {
            _feedRepository = feedRepository ?? throw new ArgumentNullException(nameof(feedRepository));
        }
        public Task<FeedModel> Handle(GetFeedPropertiesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return Task.FromResult(_feedRepository.GetFeedById(request.FeedId));
        }
    }
}
