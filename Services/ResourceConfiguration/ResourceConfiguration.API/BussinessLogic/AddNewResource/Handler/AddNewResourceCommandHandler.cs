using MediatR;
using ResourceConfiguration.API.BussinessLogic.AddNewResource.Command;
using ResourceConfigurator.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.BussinessLogic.AddNewResource.Handler
{
    public class AddNewResourceCommandHandler : IRequestHandler<AddNewResourceCommand>
    {
        private readonly newsaggregatorresourceContext _database;

        public AddNewResourceCommandHandler(newsaggregatorresourceContext database)
        {
            _database = database;
        }

        public Task<Unit> Handle(AddNewResourceCommand request, CancellationToken cancellationToken)
        {
            _database.Resource.Add(new Resource()
            {
                Url = request.Uri,
                Active = true
            });

            _database.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
