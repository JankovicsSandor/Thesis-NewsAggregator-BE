using EventBusRabbitMQ.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Writer.Shared.Events;

namespace Writer.BussinessLogic.EventHandler
{
    public class NewsGroupDoneEventHandler : IIntegrationEventHandler<NewsGroupDoneEvent>
    {
        public Task Handle(NewsGroupDoneEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
