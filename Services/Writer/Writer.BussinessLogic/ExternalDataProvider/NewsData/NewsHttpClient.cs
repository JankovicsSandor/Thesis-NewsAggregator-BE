using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Writer.Shared.External;

namespace Writer.BussinessLogic.ExternalDataProvider.NewsData
{
    public class NewsHttpClient : INewsHttpClient
    {
        private HttpClient _client;
        private ILogger<NewsHttpClient> _logger;

        public NewsHttpClient(HttpClient client, ILogger<NewsHttpClient> logger)
        {
            _client = client;
            string url = Environment.GetEnvironmentVariable("APPSETTING_NEWS_URL");
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("News BaseUrl is empty");
            }
            _client.BaseAddress = new Uri(url);
            _logger = logger;
        }

        public async Task<GetNewsArticleByDescriptionResponse> GetArticleFromDescription(string description)
        {
            HttpResponseMessage actualArticleRequest = await _client.GetAsync($"article/byDescription?description={description}");
            string content = await actualArticleRequest.Content.ReadAsStringAsync();
            if (!actualArticleRequest.IsSuccessStatusCode)
            {
                // TODO handle failure
                _logger.LogError("Get news by description failed:" + content);
            }

            GetNewsArticleByDescriptionResponse actualArticle = JsonConvert.DeserializeObject<GetNewsArticleByDescriptionResponse>(content);

            return actualArticle;
        }
    }
}
