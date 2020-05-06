using NewsReader.API.Models.Output;
using System.Threading.Tasks;

namespace NewsReader.API.NetworkClient
{
    public interface IGetResourceNetworkClient
    {
        ResourcePropertiesModel GetResourceProperties(string url);
    }
}