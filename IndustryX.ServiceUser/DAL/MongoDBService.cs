using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Models.Enums;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IndustryX.ServiceUser.DAL
{
    public class MongoDBService
    {
        public static readonly ConfigurationBuilder configuration = (ConfigurationBuilder)new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        public static readonly IConfigurationRoot configurationRoot = configuration.Build();
        static readonly string connectionString = configurationRoot["ConnectionStrings:MongoDB"];
        public readonly IMongoCollection<User> _userCollection;

       

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
        }
        public MongoDBService(AccessedCollection accessedCollection) 
        {
            switch (accessedCollection)
            {
                case AccessedCollection.User:
                    MongoClient client = new MongoClient(connectionString);
                    IMongoDatabase database = client.GetDatabase("USERDB");
                    _userCollection = database.GetCollection<User>("users");
                    break;
            }
            
        }
    }
}
