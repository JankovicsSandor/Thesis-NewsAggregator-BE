﻿using MediatR;
using News.API.BussinessLogic.AddNewArticle.Command;
using News.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace News.API.BussinessLogic.AddNewArticle.Handler
{
    public class AddNewArticleHandler : IRequestHandler<AddNewArticleCommand>
    {
        private readonly newsaggregatordataContext _context;

        public AddNewArticleHandler(newsaggregatordataContext context)
        {
            _context = context;
        }

        // TODO check into possibilities of batch update
        public Task<Unit> Handle(AddNewArticleCommand request, CancellationToken cancellationToken)
        {
            Article newItem = new Article()
            {
                Title=request.Title,
                Description=request.Description,
                PublishDate=request.PublishDate,
                Link=request.Link,
                FeedId=request.FeedId,
                Picture=request.Picture
            };

            _context.Article.Add(newItem);
            _context.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}
