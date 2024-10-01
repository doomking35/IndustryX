using Amazon.Util.Internal.PlatformServices;
using IndustryX.InfrastructureModels;
using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Models.Enums;
using IndustryX.ServiceUser.Repositories.Interfaces;
using MassTransit;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IndustryX.ServiceUser.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient client, IOptions<MongoDBSettings> mongoDBSettings)
        {
            var settings = mongoDBSettings.Value;
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(ObjectId id)
        {
            return await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(ObjectId id, User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == id, user);
        }

        public async Task DeleteUserAsync(ObjectId id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }
    }
}