using System;
using System.Collections.Generic;

namespace News.DataAccess.Database
{
    public partial class Feed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public string Picture { get; set; }
    }
}
