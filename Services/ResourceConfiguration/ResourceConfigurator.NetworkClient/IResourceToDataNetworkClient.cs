using ResourceConfigurator.Shared.Event;
using System.Threading.Tasks;

namespace ResourceConfigurator.NetworkClient
{
    public interface IResourceToDataNetworkClient
    {
        Task AddNewArticleToData();
        Task<int> AddNewResourceToData(AddNewResourceEvent newItem);
    }
}