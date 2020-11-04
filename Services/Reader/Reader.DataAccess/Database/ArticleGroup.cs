using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Reader.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.DataAccess.Database
{
    public class ArticleGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Similar")]
        public Article Similar { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
