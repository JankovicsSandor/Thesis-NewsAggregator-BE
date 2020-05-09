using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceConfigurator.NetworkClient
{
    public class ResourceToDataNetworkClient
    {
        private HttpClient _client;

        public ResourceToDataNetworkClient(HttpClient client)
        {
            _client = client;
            string url= Environment.GetEnvironmentVariable("DATA_URL"); ;
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("BaseUrl is empty");
            }
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("DATA_URL"));
        }

        public async Task AddNewArticleToData()
        {

        }



    }
}
