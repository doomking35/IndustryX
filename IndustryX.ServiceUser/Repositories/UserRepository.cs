using Amazon.Util.Internal.PlatformServices;
using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Models.Enums;
using IndustryX.ServiceUser.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IndustryX.ServiceUser.Repositories
{
    public class UserRepository : MongoDBService, IUserRepository
    {
        //public static readonly ConfigurationBuilder configuration = (ConfigurationBuilder)new ConfigurationBuilder().AddJsonFile("appsettings.json");
        //public static readonly IConfigurationRoot configurationRoot = configuration.Build();
        //static readonly string connectionString = configurationRoot["ConnectionStrings:mongoDB"];
        //private readonly IMongoCollection<User> client = new MongoClient(connectionString).GetDatabase("USERDB").GetCollection<User>("users");

        private readonly MongoDBService _service;
        public UserRepository(MongoDBService mongoDBService) : base(AccessedCollection.User) {
            _service = mongoDBService;
        }
        public UserRepository() : base(AccessedCollection.User) {
            _service = new MongoDBService(null);
        }

        public User GetById(int id)
        {
            throw new Exception("Obsolete Function; TODO: ID stores as ObjectID not an Integer value anymore");
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            return _service._userCollection.Find(filter).FirstOrDefault();
        }
        public void Add(User user)
        {
            _service._userCollection.InsertOne(user);           
        }
        public void Delete(int id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            _service._userCollection.DeleteOne(filter);
        }
        public void Update(User user)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", user.Id);
            _service._userCollection.ReplaceOne(filter, user);            
        }
        public User GetByUserName(string userName)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserName", userName);
            return _service._userCollection.Find(filter).FirstOrDefault();
        }
        public User GetStatusByUserName(string userName)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserName", userName);       
            return _service._userCollection.Find(filter).FirstOrDefault();
        }
    }

}