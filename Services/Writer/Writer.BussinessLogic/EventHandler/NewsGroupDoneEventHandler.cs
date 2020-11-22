using AutoMapper;
using EventBusRabbitMQ.Abstractions;
using News.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Writer.BussinessLogic.ExternalDataProvider.NewsData;
using Writer.DataAccess.Database;
using Writer.Shared.Events;

namespace Writer.BussinessLogic.EventHandler
{
    public class NewsGroupDoneEventHandler : IIntegrationEventHandler<NewsGroupDoneEvent>
    {
        private INewsHttpClient _newsClient;
        private readonly IMongoDatabaseService _mongoService;

        public NewsGroupDoneEventHandler(INewsHttpClient newsHttpClient, IMongoDatabaseService mongoService)
        {
            _newsClient = newsHttpClient;
            _mongoService = mongoService;
        }

        /// <summary>
        /// Handles the news groupping done event sent by the news similarity python app
        /// </summary>
        /// <param name="event">Event properties</param>
        /// <returns></returns>
        public Task Handle(NewsGroupDoneEvent @event)
        {
            // TODO handle the similarities
            ArticleGroup newGroup = new ArticleGroup()
            {
                CreateDate = DateTime.Now,
                Similar = new List<Article>() { 
                new Article()
                    {
                        NewsID = @event.Id.ToString(),
                        Description = @event.NewsItem.Description,
                        Link = @event.NewsItem.Description,
                        Picture = @event.NewsItem.Picture,
                        PublishDate = @event.NewsItem.PublishDate,
                        Title = @event.NewsItem.Title,
                        FeedName = @event.NewsItem.FeedName,
                        FeedPicture = @event.NewsItem.FeedPicture
                    }
                }
            };

            newGroup.LatestArticleDate = newGroup.Similar.Max(e => e.PublishDate);
            _mongoService.AddArticleGroup(newGroup);

            return Task.FromResult(@event.NewsItem.Id);
        }
    }
}
