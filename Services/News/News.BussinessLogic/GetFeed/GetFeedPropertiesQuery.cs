using MediatR;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.GetFeed
{
    public class GetFeedPropertiesQuery : IRequest<FeedModel>
    {
        public int FeedId { get; set; }
    }
}
