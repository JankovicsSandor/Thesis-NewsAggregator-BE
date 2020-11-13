using AutoMapper;
using EventBusRabbitMQ.Abstractions;
using EventBusRabbitMQ.Events;
using News.DataAccess.Database;
using News.DataAccess.Repository;
using News.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.BussinessLogic.Event
{
    public class AddNewArticleEventHandler : IIntegrationEventHandler<AddNewArticleEvent>
    {
        private IMapper _mapper;
        private readonly IArticleRepository _articleRepo;

        public AddNewArticleEventHandler(IMapper mapper, IArticleRepository articleRepo)
        {
            _mapper = mapper;
            _articleRepo = articleRepo;
        }
        public async Task Handle(AddNewArticleEvent @event)
        {
            Article newArticle = _mapper.Map<Article>(@event);
            await _articleRepo.AddNewArticle(newArticle);
        }
    }
}
