using IndustryX.ServiceUser.Models;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Services.Interfaces
{
    public interface IUserService : IService
    {
        User GetUserByUserName(string username);
        User GetUserById(int id);
        User GetStatusByUserName(string username);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
