using MediatR;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.API.BussinessLogic.AddNewResource.Command
{
    public class AddNewResourceCommand:IRequest<NewResourceModel>
    {
        public string ResourceName { get; set; }
        public string PictureUrl { get; set; }
    }
}
