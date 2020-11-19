using News.Shared.Models;

namespace News.DataAccess.Repository
{
    public interface IFeedRepository
    {
        FeedModel GetFeedById(int feedId);
    }
}