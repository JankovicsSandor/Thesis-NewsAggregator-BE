using ResourceConfigurator.Shared.Event;
using ResourceConfigurator.Shared.Models.External;
using System.Threading.Tasks;

namespace ResourceConfigurator.NetworkClient
{
    public interface IResourceToDataNetworkClient
    {
        Task AddNewArticleToData(AddNewArticleEvent newItem);
        Task<int> AddNewResourceToData(AddNewResourceEvent newItem);
        Task<FeedModel> GetFeedPropertiesFromId(int feedId);
    }
}