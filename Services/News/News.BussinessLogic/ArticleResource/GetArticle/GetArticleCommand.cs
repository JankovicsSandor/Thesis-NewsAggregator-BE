using MediatR;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.ArticleResource.GetArticle
{
    public class GetArticleCommand : IRequest<ArticleListResponse>
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string Contains { get; set; }
        public string Agency { get; set; }
        public int Page { get; set; }
    }
}
