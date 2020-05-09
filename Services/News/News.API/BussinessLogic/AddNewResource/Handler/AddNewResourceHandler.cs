using MediatR;
using News.API.BussinessLogic.AddNewResource.Command;
using News.DataAccess.Database;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.API.BussinessLogic.AddNewResource.Handler
{
    public class AddNewResourceHandler : IRequestHandler<AddNewResourceCommand, NewResourceModel>
    {
        private readonly newsaggregatordataContext _context;

        public AddNewResourceHandler(newsaggregatordataContext context)
        {
            _context = context;
        }
        public Task<NewResourceModel> Handle(AddNewResourceCommand request, CancellationToken cancellationToken)
        {
            Feed newFeed = new Feed()
            {
                Active = true,
                Name = request.ResourceName,
                Picture = request.PictureUrl
            };
            _context.Feed.Add(newFeed);
            _context.SaveChanges();

            return Task.FromResult(new NewResourceModel() { Id = newFeed.Id });
        }
    }
}
