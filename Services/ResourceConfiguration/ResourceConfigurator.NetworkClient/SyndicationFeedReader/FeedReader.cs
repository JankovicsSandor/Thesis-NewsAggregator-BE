using ResourceConfiguration.API.Models.Output;
using ResourceConfigurator.Shared.Event;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // TODO make function effective with removing old items
        public IEnumerable<AddNewArticleEvent> GetFeedContent(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            IEnumerable<AddNewArticleEvent> articles = feed.Items.Select(item => new AddNewArticleEvent()
            {
                Description = item.Summary.Text,
                Title = item.Title.Text,
                Link = item.Links.FirstOrDefault().Uri.ToString(),
                PublishDate = item.PublishDate.DateTime,
                // TODO Add picture
            });

            return articles;
        }
    }
}
