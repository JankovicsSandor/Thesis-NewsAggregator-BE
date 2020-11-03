using ResourceConfiguration.API.Models.Output;
using ResourceConfigurator.NetworkClient.MetaData;
using ResourceConfigurator.Shared.Event;
using System;
using System.Collections.Concurrent;
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
        private IMetaDataReader _metaDataReader;

        public FeedReader(IMetaDataReader metaReader)
        {
            _metaDataReader = metaReader;
        }

        public ResourcePropertiesModel GetResourceProperties(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            SyndicationItem item = feed.Items.FirstOrDefault();
            return new ResourcePropertiesModel()
            {
                Description = item.Summary.Text,
                Title = item.Title.Text,
                Link = item.Links.FirstOrDefault().Uri.ToString(),
                PublishDate = item.PublishDate.DateTime,
                Author = new ResourceProvider()
                {
                    Picture = feed.ImageUrl.ToString()
                }

            };
        }

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

            ConcurrentBag<AddNewArticleEvent> articles = new ConcurrentBag<AddNewArticleEvent>();

            //TODO check for paralell execution
            foreach (var currentArticle in feed.Items)
            {
                string newsUrl = currentArticle.Links.FirstOrDefault().Uri.ToString();
                AddNewArticleEvent result = new AddNewArticleEvent()
                {
                    Description = currentArticle.Summary.Text,
                    Title = currentArticle.Title.Text,
                    Link = currentArticle.Links.FirstOrDefault().Uri.ToString(),
                    PublishDate = currentArticle.PublishDate.DateTime,
                    // TODO Add picture
                    Picture = _metaDataReader.GetWebsiteMetadata(newsUrl).ImageUrl
                };
            }
            return articles;
        }
    }
}
