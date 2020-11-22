using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reader.DataAccess.Database
{
    public class ArticleGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Similar")]
        public Article Similar { get; set; }

        public DateTime CreateDate { get; set; }

        [BsonElement("LatestDate")]
        public DateTime LatestDate { get; set; }
    }
}
