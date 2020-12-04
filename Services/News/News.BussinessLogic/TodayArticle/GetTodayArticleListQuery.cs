using MediatR;
using News.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.TodayArticle
{
    public class GetTodayArticleListQuery:IRequest<IList<NewsListModel>>
    {
    }
}
