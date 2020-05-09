using ResourceConfigurator.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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


        private async Task ProcessOneResource(Resource actualItem)
        {

           // wg
        }
    }
}
