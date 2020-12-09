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

        // TODO add unit test
        /// <summary>
        /// Handles the news groupping done event sent by the news similarity python app
        /// </summary>
        /// <param name="event">Event properties</param>
        /// <returns></returns>
        public async Task Handle(NewsGroupDoneEvent @event)
        {

            if (!string.IsNullOrEmpty(@event.Similarity))
            {
                DateTime minGroupDate = DateTime.Now.Date.AddDays(-2);
                IList<ArticleGroup> articleGroups = _mongoService.GetArticleGroupsFromDateTime(minGroupDate);
                GetNewsArticleByDescriptionResponse similarArticle = await _newsClient.GetArticleFromGUID(@event.Similarity);
                if (articleGroups != null)
                {
                    ArticleGroup groupContainArticle = articleGroups.FirstOrDefault(e => e.Similar.Any(article => article.NewsID == similarArticle?.Guid));
                    //Similar article object from the news database
                    if (similarArticle != null)
                    {
                        if (groupContainArticle != null)
                        {
                            Article newArticle = new Article()
                            {
                                NewsID = @event.Id.ToString(),
                                Description = @event.NewsItem.Description,
                                Link = @event.NewsItem.Link,
                                Picture = @event.NewsItem.Picture,
                                PublishDate = @event.NewsItem.PublishDate,
                                Title = @event.NewsItem.Title,
                                FeedName = @event.NewsItem.FeedName,
                                FeedPicture = @event.NewsItem.FeedPicture
                            };
                            groupContainArticle.Similar.Add(newArticle);
                            groupContainArticle.LatestArticleDate = @event.NewsItem.PublishDate;
                            _mongoService.UpdateArticleGroup(groupContainArticle);
                        }
                        // Similar article is not in the database. Have to create a chroup with the two items
                        else
                        {
                            // Article doesn't fit into any of the groups. Have to create a new group
                            ArticleGroup newGroup = new ArticleGroup()
                            {
                                CreateDate = DateTime.Now,
                                Similar = new List<Article>() {
                                    //First article is the new article
                                    new Article()
                                    {
                                        NewsID = @event.Id.ToString(),
                                        Description = @event.NewsItem.Description,
                                        Link = @event.NewsItem.Link,
                                        Picture = @event.NewsItem.Picture,
                                        PublishDate = @event.NewsItem.PublishDate,
                                        Title = @event.NewsItem.Title,
                                        FeedName = @event.NewsItem.FeedName,
                                        FeedPicture = @event.NewsItem.FeedPicture
                                    },
                                    // Second article is the similar article
                                    new Article()
                                    {
                                        NewsID =similarArticle.Guid,
                                        Description = similarArticle.Description,
                                        Link = similarArticle.Link,
                                        Picture =similarArticle.Picture,
                                        PublishDate = similarArticle.PublishDate,
                                        Title = similarArticle.Title,
                                        FeedName = similarArticle.FeedName,
                                        FeedPicture = similarArticle.FeedPicture
                                    }
                                }
                            };
                            newGroup.LatestArticleDate = @event.NewsItem.PublishDate;
                            _mongoService.AddArticleGroup(newGroup);
                        }
                    }
                }
                else
                {
                    ArticleGroup newGroup = new ArticleGroup()
                    {
                        CreateDate = DateTime.Now,
                        Similar = new List<Article>() {
                                    new Article()
                                    {
                                        NewsID = @event.Id.ToString(),
                                        Description = @event.NewsItem.Description,
                                        Link = @event.NewsItem.Link,
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
            else
            {
                // Article doesn't have any similarity. Have to create a new group
                ArticleGroup newGroup = new ArticleGroup()
                {
                    CreateDate = DateTime.Now,
                    Similar = new List<Article>() {
                                    new Article()
                                    {
                                        NewsID = @event.Id.ToString(),
                                        Description = @event.NewsItem.Description,
                                        Link = @event.NewsItem.Link,
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
