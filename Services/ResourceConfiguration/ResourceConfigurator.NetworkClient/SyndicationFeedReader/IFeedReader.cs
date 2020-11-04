using ResourceConfiguration.API.Models.Output;
using ResourceConfigurator.Shared.Event;
using System.Collections.Generic;

namespace ResourceConfigurator.NetworkClient.SyndicationFeedReader
{
    public interface IFeedReader
    {
        IEnumerable<AddNewArticleEvent> GetFeedContent(string url);
        string GetResourceProfilePicture(string url);
        ResourcePropertiesModel GetResourceProperties(string url);
    }
}