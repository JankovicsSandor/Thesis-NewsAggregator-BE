using MediatR;
using News.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.API.BussinessLogic.GetArticle.Command
{
    public class GetArticleCommand : IRequest<IQueryable<NewsResponse>>
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string Contains { get; set; }
        public string Agency { get; set; }
        public int Page { get; set; }
    }
}
