using System;
using System.Collections.Generic;
using System.Text;

namespace Reader.Shared.Response
{
    public class HomePageNewsItem
    {
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string FeedName { get; set; }
        public string FeedPicture { get; set; }
    }
}
