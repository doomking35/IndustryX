using IndustryX.ServiceUser.Models;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Services.Interfaces
{
    public interface IUserService : IService
    {
        User GetUserByUserName(string username);
        User GetUserById(ObjectId id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(ObjectId id);
        UserConfirmation GetStatusByUserName(string username);
    }
}
