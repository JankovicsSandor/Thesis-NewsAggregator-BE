using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Writer.DataAccess.Database
{
    public partial class Feed
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonElement("Name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Active")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Active { get; set; }

        [BsonElement("Picture")]
        [BsonRepresentation(BsonType.String)]
        public string Picture { get; set; }
    }
}
