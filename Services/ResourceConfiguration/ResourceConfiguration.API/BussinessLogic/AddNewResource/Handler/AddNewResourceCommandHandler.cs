using MediatR;
using ResourceConfiguration.API.BussinessLogic.AddNewResource.Command;
using ResourceConfigurator.DataAccess.Database;
using ResourceConfigurator.NetworkClient;
using ResourceConfigurator.NetworkClient.SyndicationFeedReader;
using ResourceConfigurator.Shared.Event;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.BussinessLogic.AddNewResource.Handler
{
    public class AddNewResourceCommandHandler : IRequestHandler<AddNewResourceCommand>
    {
        private readonly newsaggregatorresourceContext _database;
        private readonly IResourceToDataNetworkClient _eventClient;
        private readonly IFeedReader _reader;

        public AddNewResourceCommandHandler(newsaggregatorresourceContext database, IFeedReader reader,
            IResourceToDataNetworkClient eventClient)
        {
            _database = database;
            _eventClient = eventClient;
            _reader = reader;
        }

        public async Task<Unit> Handle(AddNewResourceCommand request, CancellationToken cancellationToken)
        {
            string feedPicture = _reader.GetResourceProfilePicture(request.Url);
            int feedId = await _eventClient.AddNewResourceToData(new AddNewResourceEvent() { ResourceName = request.Name, PictureUrl = feedPicture });
            _database.Resource.Add(new Resource()
            {
                Url = request.Url,
                Active = true,
                FeedId = feedId
            });

            _database.SaveChanges();

            return Unit.Value;
        }
    }
}
