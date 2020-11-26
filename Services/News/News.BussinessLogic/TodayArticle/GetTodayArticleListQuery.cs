using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BussinessLogic.TodayArticle
{
    public class GetTodayArticleListQuery:IRequest<IList<string>>
    {
    }
}
