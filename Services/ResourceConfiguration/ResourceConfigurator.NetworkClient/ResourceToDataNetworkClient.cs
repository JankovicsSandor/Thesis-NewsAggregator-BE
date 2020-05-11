using ResourceConfigurator.Shared.Event;
using ResourceConfigurator.Shared.Models.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;

namespace ResourceConfigurator.NetworkClient
{
    public class ResourceToDataNetworkClient : IResourceToDataNetworkClient
    {
        private HttpClient _client;

        public ResourceToDataNetworkClient(HttpClient client)
        {
            _client = client;
            string url = Environment.GetEnvironmentVariable("APPSETTING_DATA_URL");
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("BaseUrl is empty");
            }
            _client.BaseAddress = new Uri(url);
        }

        public async Task AddNewArticleToData(AddNewArticleEvent newItem)
        {
            HttpResponseMessage response = await _client.PostAsync("api/article", new StringContent(JsonSerializer.Serialize(newItem), Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task<int> AddNewResourceToData(AddNewResourceEvent newItem)
        {
            HttpResponseMessage response = await _client.PostAsync("api/resource", new StringContent(JsonSerializer.Serialize(newItem), Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            AddNewResourceEventResponse responseContent = JsonSerializer.Deserialize<AddNewResourceEventResponse>(content);

            return responseContent.Id;


        }


    }
}
