using System;
using System.Collections.Generic;
using System.Text;

namespace Reader.Shared.Response
{
    public class HomePageNewsGroup
    {
        public IList<HomePageNewsItem> NewsItem { get; set; }

        public HomePageNewsGroup()
        {
            NewsItem = new List<HomePageNewsItem>();
        }
    }
}
