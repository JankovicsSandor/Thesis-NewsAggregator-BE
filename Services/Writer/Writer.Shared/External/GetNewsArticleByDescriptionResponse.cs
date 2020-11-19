using System;
using System.Collections.Generic;
using System.Text;

namespace Writer.Shared.External
{
    public class GetNewsArticleByDescriptionResponse
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FeedName { get; set; }
        public string FeedPicture { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }

    }
}
