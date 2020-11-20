using EventBusRabbitMQ.Events;
using System;

namespace ResourceConfigurator.Shared.Event
{
    public class AddNewArticleEvent : IntegrationEvent
    {
        public int FeedId { get; set; }
        public string FeedName { get; set; }
        public string FeedPicture { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }
    }
}
