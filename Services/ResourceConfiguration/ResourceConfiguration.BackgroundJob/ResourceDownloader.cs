using ResourceConfigurator.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResourceConfiguration.BackgroundJob
{
    public class ResourceDownloader
    {
        private readonly newsaggregatorresourceContext _databasecontext;

        public ResourceDownloader(newsaggregatorresourceContext databasecontext)
        {
            _databasecontext = databasecontext;
        }


        public async Task ProcessResources()
        {

           
        }
    }
}
