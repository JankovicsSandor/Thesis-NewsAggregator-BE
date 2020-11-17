using MediatR;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.GetArticle
{
    public class GetArticleByDescriptionCommand : IRequest<NewsItemResponse>
    {
        public string Description { get; set; }
    }
}
