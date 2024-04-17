using IndustryX.ServiceUser.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IndustryX.ServiceUser.DAL
{
    public class MongoDBService
    {
        private readonly IMongoCollection<User> _userCollection;

        public MongoDBService()
        {
        }

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
        }
    }
}
