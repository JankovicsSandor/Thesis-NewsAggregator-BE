﻿using MongoDB.Bson;
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
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Similar")]
        public Article Similar { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
