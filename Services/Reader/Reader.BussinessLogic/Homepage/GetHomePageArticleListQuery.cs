using MediatR;
using Reader.Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reader.BussinessLogic.Homepage
{
    public class GetHomePageArticleListQuery:IRequest<IList<HomePageNewsGroup>>
    {
    }
}
