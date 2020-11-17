using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Writer.Shared.Events
{
    public class NewsGroupDoneEvent : IntegrationEvent
    {
        public IEnumerable<string> Similar { get; set; }
    }
}
