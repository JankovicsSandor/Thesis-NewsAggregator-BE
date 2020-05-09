using System;
using System.Collections.Generic;

namespace News.DataAccess.Database
{
    public partial class Article
    {
        public int Id { get; set; }
        public int FeedId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }
    }
}
