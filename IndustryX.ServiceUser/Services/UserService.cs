using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Repositories;
using IndustryX.ServiceUser.Services.Interfaces;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Service
{
    public class UserService : IUserService
    {
        public readonly UserRepository repository = new();
        public User GetUserById(ObjectId id) => repository.GetById(id);
        public User GetUserByUserName(string username) => repository.GetByUserName(username);
        public void CreateUser(User user) => repository.Add(user);
        public void DeleteUser(ObjectId id) => repository.Delete(id);
        public string GetName()
        {
            throw new NotImplementedException();
        }
        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public UserConfirmation GetStatusByUserName(string username) => repository.GetStatusByUserName(username);

    }
}
