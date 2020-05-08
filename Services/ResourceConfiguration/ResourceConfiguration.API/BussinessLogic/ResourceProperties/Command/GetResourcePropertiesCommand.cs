using MediatR;
using ResourceConfiguration.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.BussinessLogic.ResourceProperties.Command
{
    public class GetResourcePropertiesCommand : IRequest<ResourcePropertiesModel>
    {
        public string Url { get; set; }
    }
}
