using Amazon.Util.Internal.PlatformServices;
using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Models.Enums;
using IndustryX.ServiceUser.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IndustryX.ServiceUser.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static readonly ConfigurationBuilder configuration = (ConfigurationBuilder)new ConfigurationBuilder().AddJsonFile("appsettings.json");
        public static readonly IConfigurationRoot configurationRoot = configuration.Build();
        static readonly string connectionString = configurationRoot["ConnectionStrings:mongoDB"];
        private readonly IMongoCollection<User> client = new MongoClient(connectionString).GetDatabase("USERDB").GetCollection<User>("users");
        //private readonly IMongoCollection<UserConfirmation> clientConfirmation = new MongoClient(connectionString).GetDatabase("USERDB").GetCollection<UserConfirmation>("usersConfirmation");

        //private readonly MongoDBService _mongoDBService;
        public UserRepository() { }

        public User GetById(int id)
        {
            throw new Exception("Obsolete Function; TODO: ID stores as ObjectID not an Integer value anymore");
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            return client.Find(filter).FirstOrDefault();
        }
        public void Add(User user)
        {
            client.InsertOne(user);           
        }
        public void Delete(int id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            client.DeleteOne(filter);
        }
        public void Update(User user)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", user.Id);
            client.ReplaceOne(filter, user);            
        }
        public User GetByUserName(string userName)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserName", userName);
            return client.Find(filter).FirstOrDefault();
        }
        public User GetStatusByUserName(string userName)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserName", userName);       
            return client.Find(filter).FirstOrDefault();
        }
    }

}