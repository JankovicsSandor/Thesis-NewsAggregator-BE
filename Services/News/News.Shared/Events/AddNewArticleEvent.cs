using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Shared.Events
{
    public class AddNewArticleEvent : IntegrationEvent
    {
        public int FeedId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }
    }
}
