using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Writer.DataAccess.Database;

namespace News.DataAccess.Database
{
    public class ArticleGroup
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Similar")]
        public IEnumerable<Article> Similar { get; set; }

        public DateTime CreateDate { get; set; }

        [BsonElement("LatestDate")]
        public DateTime LatestArticleDate { get; set; }

        public ArticleGroup()
        {
            _id = ObjectId.GenerateNewId();
        }
    }
}
