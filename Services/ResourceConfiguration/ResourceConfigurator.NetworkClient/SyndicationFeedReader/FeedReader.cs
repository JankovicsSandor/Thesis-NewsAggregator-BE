using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceConfigurator.NetworkClient.SyndicationFeedReader
{
    public class FeedReader : IFeedReader
    {

        public string GetResourceProfilePicture(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            return feed.ImageUrl.AbsoluteUri;
        }
    }
}
