using MediatR;
using ResourceConfiguration.API.BussinessLogic.ResourceProperties.Command;
using ResourceConfiguration.API.Models.Output;
using ResourceConfigurator.NetworkClient.SyndicationFeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.BussinessLogic.ResourceProperties.Handler
{
    public class GetResourcePropertiesHandler : IRequestHandler<GetResourcePropertiesCommand, ResourcePropertiesModel>
    {
        private IFeedReader _networkClient;

        public GetResourcePropertiesHandler(IFeedReader networkClient)
        {
            _networkClient = networkClient;
        }
        public Task<ResourcePropertiesModel> Handle(GetResourcePropertiesCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_networkClient.GetResourceProperties(request.Url));
        }
    }
}
