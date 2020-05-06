using NewsReader.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace NewsReader.API.NetworkClient
{
    public class GetResourceNetworkClient : IGetResourceNetworkClient
    {

        public GetResourceNetworkClient()
        {
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
                Title=item.Title.Text,
                Link = item.Links.FirstOrDefault().Uri.ToString(),
                PublishDate = item.PublishDate.DateTime,
                Author = new ResourceProvider()
                {
                    Picture = feed.ImageUrl.ToString()
                }

            };
        }
    }
}
