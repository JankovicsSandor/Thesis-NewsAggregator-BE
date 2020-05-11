using System.Threading.Tasks;

namespace ResourceConfiguration.BackgroundJob.Worker
{
    public interface IResourceDownloader
    {
        Task ProcessResources();
    }
}