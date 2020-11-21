using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Writer.Shared.Events
{
    public class NewsGroupDoneEvent : IntegrationEvent
    {
        public NewsGroupDoneNewsItem NewsItem { get; set; }
        public IEnumerable<string> Similarities { get; set; }
    }
}
