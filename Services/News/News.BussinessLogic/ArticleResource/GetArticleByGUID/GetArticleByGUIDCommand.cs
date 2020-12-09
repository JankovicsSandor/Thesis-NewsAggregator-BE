using MediatR;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.ArticleResource.GetArticleByGUID
{
    public class GetArticleByGUIDCommand : IRequest<NewsItemResponse>
    {
        public string GuId { get; set; }
    }
}
