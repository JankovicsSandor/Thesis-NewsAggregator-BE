using MediatR;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.AddNewResource
{
    public class AddNewResourceCommand : IRequest<NewResourceModel>
    {
        public string ResourceName { get; set; }
        public string PictureUrl { get; set; }
    }
}
