using ResourceConfiguration.API.Models.Output;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.NetworkClient
{
    public interface IGetResourceNetworkClient
    {
        ResourcePropertiesModel GetResourceProperties(string url);
    }
}