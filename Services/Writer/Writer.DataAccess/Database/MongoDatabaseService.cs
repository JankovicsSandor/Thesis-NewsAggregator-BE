using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Writer.DataAccess.Database
{
    public class MongoDatabaseService
    {
        private IMongoDatabase _context;

        public MongoDatabaseService()
        {
            string mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_DATABASE");
            if (string.IsNullOrEmpty(mongoConnectionString))
            {
                throw new Exception("MongoConnectionstring is empty");
            }

            string mongoDatabase = Environment.GetEnvironmentVariable("MONGODB_COLLECTION_NAME");
            if (string.IsNullOrEmpty(mongoDatabase))
            {
                throw new Exception("MongoDatabase name is empty");
            }
            var client = new MongoClient(mongoConnectionString);
            _context = client.GetDatabase(mongoDatabase);
        }
    }
}
