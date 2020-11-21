using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Writer.DataAccess.Database
{
    public partial class Article
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Title")]
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }

        [BsonElement("Description")]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("Link")]
        [BsonRepresentation(BsonType.String)]
        public string Link { get; set; }

        [BsonElement("PublishDate")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime PublishDate { get; set; }

        [BsonElement("Picture")]
        [BsonRepresentation(BsonType.String)]
        public string Picture { get; set; }


        [BsonElement("FeedName")]
        [BsonRepresentation(BsonType.String)]
        public string FeedName { get; set; }


        [BsonElement("FeedPicture")]
        [BsonRepresentation(BsonType.String)]
        public string FeedPicture { get; set; }


        [BsonElement("NewsId")]
        [BsonRepresentation(BsonType.String)]
        public string NewsID { get; set; }
    }
}
