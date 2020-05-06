using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsReader.API.Models.Output
{
    public class ResourcePropertiesModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }

        public ResourceProvider Author { get; set; }
    }
}
