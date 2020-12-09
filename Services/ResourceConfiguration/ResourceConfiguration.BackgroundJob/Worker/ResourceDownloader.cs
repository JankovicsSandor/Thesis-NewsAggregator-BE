using EventBusRabbitMQ.Abstractions;
using Microsoft.Extensions.Logging;
using ResourceConfigurator.DataAccess.Database;
using ResourceConfigurator.NetworkClient;
using ResourceConfigurator.NetworkClient.SyndicationFeedReader;
using ResourceConfigurator.Shared.Event;
using ResourceConfigurator.Shared.Models.External;
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
        private readonly IResourceToDataNetworkClient _resourceClient;
        // TODO write unit test
        public ResourceDownloader(newsaggregatorresourceContext databasecontext, IResourceToDataNetworkClient resourceNetworkClient, IFeedReader reader, IEventBus eventBus, ILogger<ResourceDownloader> logger)
        {
            _databasecontext = databasecontext ?? throw new ArgumentNullException(nameof(databasecontext));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _eventHub = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _resourceClient = resourceNetworkClient ?? throw new ArgumentNullException(nameof(resourceNetworkClient));
        }


        public async Task ProcessResources()
        {
            List<Resource> resourceList = _databasecontext.Resource.Where(e => e.FeedId != null).ToList();
            // TODO check if resources can work paralell
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
            FeedModel actualFeed = await _resourceClient.GetFeedPropertiesFromId(actualItem.FeedId.Value);
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
                        item.FeedName = actualFeed.Name;
                        item.FeedPicture = actualFeed.Picture;
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
                        item.FeedName = actualFeed.Name;
                        item.FeedPicture = actualFeed.Picture;

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
