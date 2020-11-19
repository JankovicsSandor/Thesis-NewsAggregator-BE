using News.DataAccess.Database;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace News.DataAccess.Repository
{
    public class FeedRepository : IFeedRepository
    {
        private newsaggregatordataContext _context;

        public FeedRepository(newsaggregatordataContext context)
        {
            _context = context;
        }
        public FeedModel GetFeedById(int feedId)
        {
            return (from feed in _context.Feed
                    where feed.Id == feedId
                    select new FeedModel()
                    {
                        Name = feed.Name,
                        Picture = feed.Picture
                    }).FirstOrDefault();
        }
    }
}
