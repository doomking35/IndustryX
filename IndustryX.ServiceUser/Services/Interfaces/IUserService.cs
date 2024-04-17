using IndustryX.ServiceUser.Models;

namespace IndustryX.ServiceUser.Services.Interfaces
{
    public interface IUserService : IService
    {
        User GetUserByUserName(string username);
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
