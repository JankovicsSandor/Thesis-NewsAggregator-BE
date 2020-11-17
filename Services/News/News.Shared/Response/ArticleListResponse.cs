using System;
using System.Collections.Generic;
using System.Text;

namespace News.Shared.Response
{
    public class ArticleListResponse
    {
        public IEnumerable<NewsResponse> Result { get; set; }
        public int Total { get; set; }
    }
}
