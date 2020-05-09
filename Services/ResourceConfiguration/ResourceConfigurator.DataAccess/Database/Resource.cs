using System;
using System.Collections.Generic;

namespace ResourceConfigurator.DataAccess.Database
{
    public partial class Resource
    {
        public Resource()
        {
            Lastsynchronizedresource = new HashSet<Lastsynchronizedresource>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public bool? Active { get; set; }
        public int FeedId { get; set; }

        public virtual ICollection<Lastsynchronizedresource> Lastsynchronizedresource { get; set; }
    }
}
