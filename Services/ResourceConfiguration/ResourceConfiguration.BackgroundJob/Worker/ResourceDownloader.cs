using EventBusRabbitMQ.Abstractions;
using Microsoft.Extensions.Logging;
using ResourceConfigurator.DataAccess.Database;
using ResourceConfigurator.NetworkClient;
using ResourceConfigurator.NetworkClient.SyndicationFeedReader;
using ResourceConfigurator.Shared.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceConfiguration.BackgroundJob.Worker
{
    public class ResourceDownloader : IResourceDownloader
    {
        private readonly newsaggregatorresourceContext _databasecontext;
        private readonly IFeedReader _reader;
        private readonly IEventBus _eventHub;
        private readonly ILogger<ResourceDownloader> _logger;

        public ResourceDownloader(newsaggregatorresourceContext databasecontext, IFeedReader reader, IEventBus eventBus, ILogger<ResourceDownloader> logger)
        {
            _databasecontext = databasecontext;
            _reader = reader;
            _eventHub = eventBus;
            _logger = logger;
        }


        public async Task ProcessResources()
        {
            List<Resource> resourceList = _databasecontext.Resource.Where(e => e.FeedId != null).ToList();
            foreach (Resource oneResource in resourceList)
            {
                await ProcessOneResource(oneResource);
            }
        }


        private async Task ProcessOneResource(Resource actualItem)
        {
            Lastsynchronizedresource lastSource = _databasecontext.Lastsynchronizedresource.FirstOrDefault(e => e.ResourceId == actualItem.Id);
            int i = 0;
            IEnumerable<AddNewArticleEvent> feedContent = _reader.GetFeedContent(actualItem.Url);
            feedContent = feedContent.Reverse();
            if (lastSource == null)
            {
                lastSource = new Lastsynchronizedresource() { ResourceId = actualItem.Id, Title = String.Empty, Description = String.Empty };
                _databasecontext.Add(lastSource);
                _databasecontext.SaveChanges();
                // New item. Not Synchornized yet.
                foreach (var item in feedContent)
                {
                    try
                    {
                        item.FeedId = actualItem.FeedId.Value;
                        // Publish the new article to the hub
                        // TODO handle failure
                        _eventHub.Publish(item);

                        lastSource.Title = item.Title;
                        lastSource.Description = item.Description;
                        _databasecontext.Update(lastSource);
                        _databasecontext.SaveChanges();

                        // TODO add integration event and pubish to hub
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Failed to add new article");
                        _logger.LogError(e.Message);
                    }
                }


            }
            else
            {
                // TODO Add update to description and grouping
                AddNewArticleEvent item = feedContent.ElementAt(i);
                while (!item.Title.Equals(lastSource.Title, StringComparison.OrdinalIgnoreCase) && !item.Description.Equals(lastSource.Description, StringComparison.OrdinalIgnoreCase))
                {
                    item = feedContent.ElementAt(i);
                    try
                    {
                        item.FeedId = actualItem.FeedId.Value;
                        _eventHub.Publish(item);
                        lastSource.Title = item.Title;
                        lastSource.Description = item.Description;
                        _databasecontext.Update(lastSource);
                        _databasecontext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Failed to add new article");
                        _logger.LogError(e.Message);
                    }

                    i++;
                }
            }
        }
    }
}
