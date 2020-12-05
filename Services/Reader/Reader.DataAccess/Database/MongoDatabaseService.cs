using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reader.DataAccess.Database
{
    public class MongoDatabaseService : IMongoDatabaseService
    {
        private IMongoDatabase _context;
        private IMongoCollection<ArticleGroup> _articleGroup;

        public MongoDatabaseService()
        {
            string mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
            if (string.IsNullOrEmpty(mongoConnectionString))
            {
                throw new Exception("MongoConnectionstring is empty");
            }

            string mongoDatabase = Environment.GetEnvironmentVariable("MONGODB_DATABASE");
            if (string.IsNullOrEmpty(mongoDatabase))
            {
                throw new Exception("MongoDatabase name is empty");
            }
            var client = new MongoClient(mongoConnectionString);
            _context = client.GetDatabase(mongoDatabase);

            string collectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION_NAME");
            if (string.IsNullOrEmpty(mongoDatabase))
            {
                throw new Exception("MongoDatabase collection is empty");
            }
            _articleGroup = _context.GetCollection<ArticleGroup>(collectionName);
        }
        public List<ArticleGroup> GetAllArticleGroup()
        {
            return _articleGroup.Find(article => true).ToList();
        }

        public List<ArticleGroup> GetHomePageArticles(DateTime minDate)
        {
            return _articleGroup.Find(article => article.LatestDate >= minDate).ToList();
        }
    }
}
