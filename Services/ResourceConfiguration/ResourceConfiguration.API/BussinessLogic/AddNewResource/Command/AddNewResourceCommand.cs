using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.BussinessLogic.AddNewResource.Command
{
    public class AddNewResourceCommand : IRequest
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
