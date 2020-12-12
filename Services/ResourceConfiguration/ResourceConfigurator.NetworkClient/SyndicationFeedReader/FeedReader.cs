﻿using ResourceConfiguration.API.Models.Output;
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
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }


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

        public IEnumerable<AddNewArticleEvent> GetFeedContent(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            ConcurrentBag<AddNewArticleEvent> articles = new ConcurrentBag<AddNewArticleEvent>();

            Parallel.ForEach(feed.Items, (currentArticle) =>
            {
                string newsUrl = currentArticle.Links.FirstOrDefault().Uri.ToString();
                // Every article must have a summary and a title
                if (currentArticle.Summary != null && currentArticle.Title != null)
                {
                    AddNewArticleEvent result = new AddNewArticleEvent()
                    {
                        Description = currentArticle.Summary.Text,
                        Title = currentArticle.Title.Text,
                        Link = currentArticle.Links.FirstOrDefault().Uri.ToString(),
                        PublishDate = currentArticle.PublishDate.DateTime,
                        Picture = _metaDataReader.GetWebsiteMetadata(newsUrl).ImageUrl
                    };
                    articles.Add(result);
                }

            });
            return articles.OrderByDescending(e => e.PublishDate);
        }
    }
}
