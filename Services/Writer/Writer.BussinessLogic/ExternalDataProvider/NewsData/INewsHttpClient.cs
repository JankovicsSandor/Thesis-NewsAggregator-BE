using System.Threading.Tasks;
using Writer.Shared.External;

namespace Writer.BussinessLogic.ExternalDataProvider.NewsData
{
    public interface INewsHttpClient
    {
        Task<GetNewsArticleByDescriptionResponse> GetArticleFromGUID(string description);
    }
}