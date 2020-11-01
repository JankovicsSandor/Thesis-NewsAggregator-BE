using HtmlAgilityPack;
using ResourceConfigurator.Shared.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceConfigurator.NetworkClient.MetaData
{
    public class MetaDataReader : IMetaDataReader
    {
        public WebsiteMetaData GetWebsiteMetadata(string url)
        {
            // Get the URL specified
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            var metaTags = document.DocumentNode.SelectNodes("//meta");
            WebsiteMetaData metaInfo = new WebsiteMetaData();
            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    var tagContent = tag.Attributes["content"];
                    var tagProperty = tag.Attributes["property"];
                    if (tagProperty != null && tagContent != null)
                    {
                        switch (tagProperty.Value.ToLower())
                        {
                            case "og:title":
                                metaInfo.Title = string.IsNullOrEmpty(metaInfo.Title) ? tagContent.Value : metaInfo.Title;
                                break;
                            case "og:description":
                                metaInfo.Description = string.IsNullOrEmpty(metaInfo.Description) ? tagContent.Value : metaInfo.Description;
                                break;
                            case "og:image":
                                metaInfo.ImageUrl = string.IsNullOrEmpty(metaInfo.ImageUrl) ? tagContent.Value : metaInfo.ImageUrl;
                                break;
                        }
                    }
                }
            }
            return metaInfo;
        }
    }
}
