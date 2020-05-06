using MediatR;
using NewsReader.API.BussinessLogic.ResourceProperties.Command;
using NewsReader.API.Models.Output;
using NewsReader.API.NetworkClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsReader.API.BussinessLogic.ResourceProperties.Handler
{
    public class GetResourcePropertiesHandler : IRequestHandler<GetResourcePropertiesCommand, ResourcePropertiesModel>
    {
        private IGetResourceNetworkClient _networkClient;

        public GetResourcePropertiesHandler(IGetResourceNetworkClient networkClient)
        {
            _networkClient = networkClient;
        }
        public Task<ResourcePropertiesModel> Handle(GetResourcePropertiesCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_networkClient.GetResourceProperties(request.Url));
        }
    }
}
