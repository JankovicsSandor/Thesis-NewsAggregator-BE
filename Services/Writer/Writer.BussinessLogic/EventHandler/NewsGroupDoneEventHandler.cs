using AutoMapper;
using EventBusRabbitMQ.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Writer.BussinessLogic.ExternalDataProvider.NewsData;
using Writer.DataAccess.Database;
using Writer.Shared.Events;
using Writer.Shared.External;

namespace Writer.BussinessLogic.EventHandler
{
    public class NewsGroupDoneEventHandler : IIntegrationEventHandler<NewsGroupDoneEvent>
    {
        private INewsHttpClient _newsClient;
        private readonly IMongoDatabaseService _mongoService;

        public NewsGroupDoneEventHandler(INewsHttpClient newsHttpClient, IMongoDatabaseService mongoService)
        {
            _newsClient = newsHttpClient ?? throw new ArgumentNullException(nameof(newsHttpClient));
            _mongoService = mongoService ?? throw new ArgumentNullException(nameof(mongoService));
        }

        /// <summary>
        /// Handles the news groupping done event sent by the news similarity python app
        /// </summary>
        /// <param name="event">Event properties</param>
        /// <returns></returns>
        public async Task Handle(NewsGroupDoneEvent @event)
        {
            //
            DateTime minGroupDate = DateTime.Now.Date.AddDays(-2);

            List<ArticleGroup> articleGroups = _mongoService.GetArticleGroupsFromDateTime(minGroupDate);
            // TODO handle the similarities
            bool articleInsertedToGroup = false;
            int i = 0;
            string actualSimilarText = "";
            if (articleGroups != null)
            {
                while (!articleInsertedToGroup && i < @event.Similarities.Count)
                {
                    actualSimilarText = @event.Similarities[i];
                    GetNewsArticleByDescriptionResponse similarArticle = await _newsClient.GetArticleFromDescription(actualSimilarText);
                    ArticleGroup groupContainArticle = articleGroups.Find(e => e.Similar.Any(article => article.NewsID == similarArticle.Guid));
                    // The new article is found in the last two days article groups.
                    // Have to add it as a similar article
                    if (groupContainArticle != null)
                    {
                        Article newArticle = new Article()
                        {
                            NewsID = @event.Id.ToString(),
                            Description = @event.NewsItem.Description,
                            Link = @event.NewsItem.Description,
                            Picture = @event.NewsItem.Picture,
                            PublishDate = @event.NewsItem.PublishDate,
                            Title = @event.NewsItem.Title,
                            FeedName = @event.NewsItem.FeedName,
                            FeedPicture = @event.NewsItem.FeedPicture
                        };
                        groupContainArticle.Similar.Add(newArticle);
                        groupContainArticle.LatestArticleDate = @event.NewsItem.PublishDate;
                        _mongoService.UpdateArticleGroup(groupContainArticle);
                        articleInsertedToGroup = true;
                    }
                    i++;
                }
            }
            // Article doesn't fit into any of the groups. Have to create a new group
            if (!articleInsertedToGroup)
            {
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
                newGroup.LatestArticleDate = @event.NewsItem.PublishDate;
                _mongoService.AddArticleGroup(newGroup);
            }
        }
    }
}
