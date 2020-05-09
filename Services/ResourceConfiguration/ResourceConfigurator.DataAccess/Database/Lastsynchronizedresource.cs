using System;
using System.Collections.Generic;

namespace ResourceConfigurator.DataAccess.Database
{
    public partial class Lastsynchronizedresource
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
