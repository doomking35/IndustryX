using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Repositories;
using IndustryX.ServiceUser.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Service
{
    public class UserService : IUserService
    {
        public readonly UserRepository repository = new(new MongoDBService(Models.Enums.AccessedCollection.User));
        public User GetUserById(int id) => repository.GetById(id);
        public User GetUserByUserName(string username) => repository.GetByUserName(username);
        public void CreateUser(User user) => repository.Add(user);
        public void DeleteUser(int id) => repository.Delete(id);
        public string GetName()
        {
            throw new NotImplementedException();
        }
        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public User GetStatusByUserName(string username) => repository.GetStatusByUserName(username);

    }
}
