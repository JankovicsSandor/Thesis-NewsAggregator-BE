using System;
using System.Collections.Generic;
using System.Text;

namespace News.Shared.Query
{
    public class ArticleQueryModel
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public string Contains { get; set; }
        public string Agency { get; set; }
    }
}
