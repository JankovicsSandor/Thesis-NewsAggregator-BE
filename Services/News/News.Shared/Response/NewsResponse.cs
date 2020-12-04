using System;
using System.Collections.Generic;
using System.Text;

namespace News.Shared.Response
{
    public class NewsResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public NewsAuthorResponse Author { get; set; }
    }
}
