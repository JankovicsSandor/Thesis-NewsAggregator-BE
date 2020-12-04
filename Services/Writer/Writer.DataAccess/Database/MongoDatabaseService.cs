using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Writer.DataAccess.Database
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

        public void AddArticleGroup(ArticleGroup newGroup)
        {
            if (newGroup != null)
            {
                _articleGroup.InsertOne(newGroup);
            }

        }

        public void UpdateArticleGroup(ArticleGroup updatedGroup)
        {
            if (updatedGroup != null)
            {
                _articleGroup.ReplaceOne(article => article._id == updatedGroup._id, updatedGroup);
            }

        }

        public IList<ArticleGroup> GetAllArticleGroup()
        {
            return _articleGroup.Find(article => true).ToList();
        }

        public IList<ArticleGroup> GetArticleGroupsFromDateTime(DateTime minDate)
        {
            return _articleGroup.Find(article => article.LatestArticleDate >= minDate).ToList();
        }

        public Article GetArticleFromGUID(string guid)
        {
            ArticleGroup articleGroup = _articleGroup.Find(article => article.Similar.Any(e => e.NewsID == guid)).FirstOrDefault();
            return articleGroup?.Similar.FirstOrDefault(e => e.NewsID == guid);
        }
    }
}
