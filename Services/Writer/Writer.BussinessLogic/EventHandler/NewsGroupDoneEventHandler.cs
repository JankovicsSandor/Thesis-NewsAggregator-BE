using EventBusRabbitMQ.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Writer.BussinessLogic.ExternalDataProvider.NewsData;
using Writer.Shared.Events;

namespace Writer.BussinessLogic.EventHandler
{
    public class NewsGroupDoneEventHandler : IIntegrationEventHandler<NewsGroupDoneEvent>
    {
        private INewsHttpClient _newsClient;

        public NewsGroupDoneEventHandler(INewsHttpClient newsHttpClient)
        {
            _newsClient = newsHttpClient;
        }

        /// <summary>
        /// Handles the news groupping done event sent by the news similarity python app
        /// </summary>
        /// <param name="event">Event properties</param>
        /// <returns></returns>
        public Task Handle(NewsGroupDoneEvent @event)
        {
            // TODO handle newsGroup done event
            //throw new NotImplementedException();
        }
    }
}
