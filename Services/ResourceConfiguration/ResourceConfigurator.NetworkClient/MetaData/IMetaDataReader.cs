using ResourceConfigurator.Shared.Models.Metadata;

namespace ResourceConfigurator.NetworkClient.MetaData
{
    public interface IMetaDataReader
    {
        WebsiteMetaData GetWebsiteMetadata(string url);
    }
}