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
        private readonly IMongoCollection<UserConfirmation> clientConfirmation = new MongoClient(connectionString).GetDatabase("USERDB").GetCollection<UserConfirmation>("usersConfirmation");

        //private readonly MongoDBService _mongoDBService;
        public UserRepository() { }

        public User GetById(ObjectId id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            return client.Find(filter).FirstOrDefault();
        }
        public void Add(User user)
        {
            client.InsertOne(user);
            clientConfirmation.InsertOne(new UserConfirmation(false,false, UserStatus.Active,user.UserGUID, user.Id));
        }
        public void Delete(ObjectId id)
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
        public UserConfirmation GetStatusByUserName(string userName)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserName", userName);
            var user = client.Find(filter).FirstOrDefault();
            if(user != null)
            {
                FilterDefinition<UserConfirmation> filterConfirmation = Builders<UserConfirmation>.Filter.Eq("UserGUID", user.UserGUID);
                return clientConfirmation.Find(filterConfirmation).FirstOrDefault();
            }
            return new UserConfirmation();
        }
    }

}